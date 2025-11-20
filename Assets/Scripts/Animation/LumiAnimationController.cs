using UnityEngine;

/// <summary>
/// PRODUCTION-READY animation controller for Lumi the Fox
/// Handles all 10 animation states with proper error handling
/// </summary>
public class LumiAnimationController : MonoBehaviour
{
    [Header("Animation References")]
    [SerializeField] private Animator animator;
    
    // Cached parameter hashes for performance
    private static readonly int IsHappy = Animator.StringToHash("IsHappy");
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsCelebrating = Animator.StringToHash("IsCelebrating");
    private static readonly int IsThinking = Animator.StringToHash("IsThinking");
    private static readonly int IsEncouraging = Animator.StringToHash("IsEncouraging");
    private static readonly int IsSad = Animator.StringToHash("IsSad");
    private static readonly int IsSleeping = Animator.StringToHash("IsSleeping");
    private static readonly int IsTalking = Animator.StringToHash("IsTalking");
    private static readonly int QuickCelebrate = Animator.StringToHash("QuickCelebrate");
    
    [Header("Animation Settings")]
    [SerializeField] private float celebrationDuration = 2f;
    [SerializeField] private float encouragementDuration = 1.5f;
    [SerializeField] private float happyDuration = 1f;
    
    private Coroutine currentAnimationRoutine;
    
    void Awake()
    {
        // Auto-get Animator if not set
        if (animator == null)
            animator = GetComponent<Animator>();
            
        if (animator == null)
            Debug.LogError("LumiAnimationController: No Animator found on " + gameObject.name);
    }
    
    void Start()
    {
        SetIdle();
    }
    
    #region PUBLIC ANIMATION API
    
    /// <summary>
    /// Return to neutral idle state
    /// </summary>
    public void SetIdle()
    {
        if (!ValidateAnimator()) return;
        
        ResetAllStates();
        Debug.Log("🦊 Lumi: Idle (neutral)");
    }
    
    /// <summary>
    /// Show happy, excited expression
    /// </summary>
    public void SetHappy(bool autoReturnToIdle = false)
    {
        if (!ValidateAnimator()) return;
        
        ResetAllStates();
        animator.SetBool(IsHappy, true);
        Debug.Log("🎉 Lumi: So happy!");
        
        if (autoReturnToIdle)
            Invoke(nameof(SetIdle), happyDuration);
    }
    
    /// <summary>
    /// Start or stop walking animation
    /// </summary>
    public void SetWalking(bool walking)
    {
        if (!ValidateAnimator()) return;
        
        animator.SetBool(IsWalking, walking);
        Debug.Log(walking ? "🚶 Lumi: Walking..." : "🛑 Lumi: Stopped walking");
    }
    
    /// <summary>
    /// Perform a jump
    /// </summary>
    public void SetJump()
    {
        if (!ValidateAnimator()) return;
        
        ResetAllStates();
        animator.SetBool(IsJumping, true);
        Debug.Log("🦘 Lumi: Jumping!");
    }
    
    /// <summary>
    /// Big celebration with effects
    /// </summary>
    public void SetCelebrate(bool autoReturnToIdle = false)
    {
        if (!ValidateAnimator()) return;
        
        ResetAllStates();
        animator.SetBool(IsCelebrating, true);
        Debug.Log("🌟 Lumi: Celebrating!");
        
        if (autoReturnToIdle)
            Invoke(nameof(SetIdle), celebrationDuration);
    }
    
    /// <summary>
    /// Instant celebration (trigger-based)
    /// </summary>
    public void QuickCelebration()
    {
        if (!ValidateAnimator()) return;
        
        animator.SetTrigger(QuickCelebrate);
        Debug.Log("⭐ Lumi: Quick celebration!");
    }
    
    /// <summary>
    /// Thoughtful, curious expression
    /// </summary>
    public void SetThinking()
    {
        if (!ValidateAnimator()) return;
        
        ResetAllStates();
        animator.SetBool(IsThinking, true);
        Debug.Log("🤔 Lumi: Thinking...");
    }
    
    /// <summary>
    /// Warm, encouraging gesture
    /// </summary>
    public void SetEncourage(bool autoReturnToIdle = false)
    {
        if (!ValidateAnimator()) return;
        
        ResetAllStates();
        animator.SetBool(IsEncouraging, true);
        Debug.Log("💚 Lumi: You can do it!");
        
        if (autoReturnToIdle)
            Invoke(nameof(SetIdle), encouragementDuration);
    }
    
    /// <summary>
    /// Gentle, empathetic sadness
    /// </summary>
    public void SetSad()
    {
        if (!ValidateAnimator()) return;
        
        ResetAllStates();
        animator.SetBool(IsSad, true);
        Debug.Log("😢 Lumi: Showing empathy");
    }
    
    /// <summary>
    /// Peaceful sleeping animation
    /// </summary>
    public void SetSleeping(bool sleeping)
    {
        if (!ValidateAnimator()) return;
        
        if (sleeping)
        {
            ResetAllStates();
            animator.SetBool(IsSleeping, true);
            Debug.Log("😴 Lumi: Going to sleep...");
        }
        else
        {
            animator.SetBool(IsSleeping, false);
            Debug.Log("🦊 Lumi: Waking up!");
        }
    }
    
    /// <summary>
    /// Talking/speaking animation
    /// </summary>
    public void SetTalking(bool talking)
    {
        if (!ValidateAnimator()) return;
        
        animator.SetBool(IsTalking, talking);
        Debug.Log(talking ? "💬 Lumi: Talking..." : "🦊 Lumi: Finished talking");
    }
    
    #endregion
    
    #region CONVENIENCE METHODS
    
    /// <summary>
    /// Perfect for correct answers - celebration then auto-return
    /// </summary>
    public void CelebrationForSuccess()
    {
        QuickCelebration();
    }
    
    /// <summary>
    /// Encouragement for attempts
    /// </summary>
    public void EncourageAttempt()
    {
        SetEncourage(true);
    }
    
    /// <summary>
    /// Gentle feedback for incorrect answers
    /// </summary>
    public void GentleIncorrectFeedback()
    {
        SetSad();
        Invoke(nameof(SetIdle), 2f);
    }
    
    /// <summary>
    /// Thinking for puzzles
    /// </summary>
    public void ShowThinking()
    {
        SetThinking();
    }
    
    #endregion
    
    #region PRIVATE HELPERS
    
    private void ResetAllStates()
    {
        if (animator == null) return;
        
        animator.SetBool(IsHappy, false);
        animator.SetBool(IsWalking, false);
        animator.SetBool(IsJumping, false);
        animator.SetBool(IsCelebrating, false);
        animator.SetBool(IsThinking, false);
        animator.SetBool(IsEncouraging, false);
        animator.SetBool(IsSad, false);
        animator.SetBool(IsSleeping, false);
        animator.SetBool(IsTalking, false);
    }
    
    private bool ValidateAnimator()
    {
        if (animator == null)
        {
            Debug.LogWarning("LumiAnimationController: Animator not assigned");
            return false;
        }
        return true;
    }
    
    #endregion
    
    #region EDITOR TESTING METHODS
    
    [ContextMenu("Test: Quick Celebration")]
    private void TestQuickCelebration() => QuickCelebration();
    
    [ContextMenu("Test: Happy")]
    private void TestHappy() => SetHappy(true);
    
    [ContextMenu("Test: Thinking")]
    private void TestThinking() => SetThinking();
    
    [ContextMenu("Test: Encourage")]
    private void TestEncourage() => SetEncourage(true);
    
    [ContextMenu("Test: Success Flow")]
    private void TestSuccessFlow()
    {
        SetThinking();
        Invoke(nameof(QuickCelebration), 1f);
    }
    
    #endregion
}