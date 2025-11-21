using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

/// <summary>
/// LumiAnimationController
/// - Modern, flexible animation API for Lumi
/// - Supports Animator crossfade or direct AnimationClip playback via PlayableGraph
/// - Designed for crisp, high-quality 2D sprite/skeletal animations with smooth blending
/// </summary>
[RequireComponent(typeof(Animator))]
public class LumiAnimationController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("Playback Settings")]
    [Tooltip("Crossfade duration when switching animations (seconds)")]
    [SerializeField] private float defaultCrossfade = 0.12f;

    [Tooltip("If true, use PlayableGraph to play raw AnimationClips (great for procedural control)")]
    [SerializeField] private bool usePlayableGraph = false;

    // Animator parameter names (cached hashes)
    private static readonly int ParamIdle = Animator.StringToHash("Idle");
    private static readonly int ParamWalk = Animator.StringToHash("Walk");
    private static readonly int ParamRun = Animator.StringToHash("Run");
    private static readonly int ParamJump = Animator.StringToHash("Jump");
    private static readonly int ParamFall = Animator.StringToHash("Fall");
    private static readonly int ParamLand = Animator.StringToHash("Land");
    private static readonly int ParamCelebrate = Animator.StringToHash("Celebrate");
    private static readonly int ParamQuickCelebrate = Animator.StringToHash("QuickCelebrate");
    private static readonly int ParamThink = Animator.StringToHash("Think");
    private static readonly int ParamEncourage = Animator.StringToHash("Encourage");

    // Optional: direct mapping of clip names (if using PlayableGraph)
    [Header("Animation Clips (optional, for PlayableGraph)")]
    public AnimationClip idleClip;
    public AnimationClip walkClip;
    public AnimationClip runClip;
    public AnimationClip jumpClip;
    public AnimationClip fallClip;
    public AnimationClip landClip;
    public AnimationClip celebrateClip;
    public AnimationClip quickCelebrateClip;
    public AnimationClip thinkClip;
    public AnimationClip encourageClip;

    private PlayableGraph playableGraph;
    private AnimationPlayableOutput playableOutput;
    private AnimationClipPlayable activePlayable;

    private Coroutine autoReturnRoutine;

    void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (animator == null) Debug.LogError("LumiAnimationController: Missing Animator", this);

        if (usePlayableGraph)
        {
            playableGraph = PlayableGraph.Create($"LumiGraph_{gameObject.name}");
            playableOutput = AnimationPlayableOutput.Create(playableGraph, "LumiOutput", animator);
        }
    }

    void OnDestroy()
    {
        if (playableGraph.IsValid())
            playableGraph.Destroy();
    }

    #region Public API - Named animations (10)

    public void PlayIdle(float crossfade = -1f)
    {
        PlayNamed(ParamIdle, idleClip, crossfade);
    }

    public void PlayWalk(bool isWalking, float crossfade = -1f)
    {
        if (!isWalking) { PlayIdle(crossfade); return; }
        PlayNamed(ParamWalk, walkClip, crossfade, true);
    }

    public void PlayRun(bool isRunning, float crossfade = -1f)
    {
        if (!isRunning) { PlayIdle(crossfade); return; }
        PlayNamed(ParamRun, runClip, crossfade, true);
    }

    public void PlayJump(float crossfade = -1f, bool autoReturn = true, float autoReturnAfter = 0.7f)
    {
        PlayNamed(ParamJump, jumpClip, crossfade);
        if (autoReturn) AutoReturnToIdle(autoReturnAfter);
    }

    public void PlayFall(float crossfade = -1f)
    {
        PlayNamed(ParamFall, fallClip, crossfade, true);
    }

    public void PlayLand(float crossfade = -1f)
    {
        PlayNamed(ParamLand, landClip, crossfade);
    }

    public void PlayCelebrate(float crossfade = -1f, bool autoReturn = true, float duration = 2f)
    {
        PlayNamed(ParamCelebrate, celebrateClip, crossfade);
        if (autoReturn) AutoReturnToIdle(duration);
    }

    public void PlayQuickCelebrate()
    {
        // Trigger-style instantaneous
        if (!ValidateAnimator()) return;
        if (usePlayableGraph && quickCelebrateClip != null) PlayClipImmediate(quickCelebrateClip, 0.02f);
        else animator.SetTrigger(ParamQuickCelebrate);
    }

    public void PlayThink(float crossfade = -1f)
    {
        PlayNamed(ParamThink, thinkClip, crossfade, true);
    }

    public void PlayEncourage(float crossfade = -1f, bool autoReturn = true, float duration = 1.2f)
    {
        PlayNamed(ParamEncourage, encourageClip, crossfade);
        if (autoReturn) AutoReturnToIdle(duration);
    }

    #endregion

    #region Internal Helpers

    private void PlayNamed(int paramHash, AnimationClip clip, float crossfade = -1f, bool loop = false)
    {
        if (!ValidateAnimator()) return;

        if (usePlayableGraph && clip != null)
        {
            PlayClip(clip, loop, crossfade < 0 ? defaultCrossfade : crossfade);
            return;
        }

        // Fallback to Animator parameter based system: assumes Animator Controller has bool or trigger with these names
        AnimatorParamToBool(paramHash, true);

        // crossfade to appropriate state if necessary
        // We assume animator has named states matching the parameter names
        if (crossfade < 0) crossfade = defaultCrossfade;
        string stateName = ParamHashToStateName(paramHash);
        if (!string.IsNullOrEmpty(stateName))
            animator.CrossFadeInFixedTime(stateName, crossfade);
    }

    // Convert cached param hash back to state name convention (simple mapping)
    private string ParamHashToStateName(int hash)
    {
        if (hash == ParamIdle) return "Idle";
        if (hash == ParamWalk) return "Walk";
        if (hash == ParamRun) return "Run";
        if (hash == ParamJump) return "Jump";
        if (hash == ParamFall) return "Fall";
        if (hash == ParamLand) return "Land";
        if (hash == ParamCelebrate) return "Celebrate";
        if (hash == ParamQuickCelebrate) return "QuickCelebrate";
        if (hash == ParamThink) return "Think";
        if (hash == ParamEncourage) return "Encourage";
        return null;
    }

    // Set that animator parameter's boolean true and clear other bools to prevent conflicts
    private void AnimatorParamToBool(int paramHash, bool setTrue)
    {
        // Reset all bools to false first
        animator.SetBool(ParamIdle, false);
        animator.SetBool(ParamWalk, false);
        animator.SetBool(ParamRun, false);
        animator.SetBool(ParamJump, false);
        animator.SetBool(ParamFall, false);
        animator.SetBool(ParamLand, false);
        animator.SetBool(ParamCelebrate, false);
        animator.SetBool(ParamThink, false);
        animator.SetBool(ParamEncourage, false);

        // now set requested
        try
        {
            animator.SetBool(paramHash, setTrue);
        }
        catch (System.Exception e)
        {
            // if paramHash isn't a bool param (e.g., trigger), ignore
            Debug.Log($"LumiAnimationController: Animator param set attempt failed: {e.Message}");
        }
    }

    private void AutoReturnToIdle(float seconds)
    {
        if (autoReturnRoutine != null) StopCoroutine(autoReturnRoutine);
        autoReturnRoutine = StartCoroutine(AutoReturnCoroutine(seconds));
    }

    private IEnumerator AutoReturnCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PlayIdle();
        autoReturnRoutine = null;
    }

    private bool ValidateAnimator()
    {
        if (animator == null)
        {
            Debug.LogWarning("LumiAnimationController: Animator not assigned", this);
            return false;
        }
        return true;
    }

    #endregion

    #region PlayableGraph Clip playback (precise control + blending)

    private void PlayClip(AnimationClip clip, bool loop = false, float fade = 0.12f)
{
    if (clip == null)
    {
        Debug.LogWarning("LumiAnimationController: PlayClip called with null clip");
        return;
    }

    if (!playableGraph.IsValid())
    {
        playableGraph = PlayableGraph.Create($"LumiGraph_{gameObject.name}");
        playableOutput = AnimationPlayableOutput.Create(playableGraph, "LumiOutput", animator);
    }

    if (activePlayable.IsValid())
        activePlayable.Destroy();

    activePlayable = AnimationClipPlayable.Create(playableGraph, clip);
    activePlayable.SetApplyFootIK(false);
    activePlayable.SetDuration(clip.length);
    activePlayable.SetTime(0);
    activePlayable.SetSpeed(1f);

    playableOutput.SetSourcePlayable(activePlayable);

    // Looping support across all Unity versions
    if (!loop && clip.isLooping)
        StartCoroutine(StopPlayableAtEnd((float)clip.length));

    if (!playableGraph.IsPlaying())
        playableGraph.Play();
}

private IEnumerator StopPlayableAtEnd(float duration)
{
    yield return new WaitForSeconds(duration);
    if (activePlayable.IsValid())
        activePlayable.SetSpeed(0f);
}


    // Play a short immediate clip (for triggers)
    private void PlayClipImmediate(AnimationClip clip, float crossfade = 0.05f)
    {
        PlayClip(clip, false, crossfade);
        // auto return to idle after clip length
        if (clip != null) AutoReturnToIdle((float)clip.length);
    }

    #endregion
}
