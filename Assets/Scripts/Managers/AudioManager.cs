using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Comprehensive audio management system
/// Handles music, SFX, voice, and audio settings
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource voiceSource;
    public AudioSource ambientSource;
    
    [Header("Music Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;
    public AudioClip victoryMusic;
    public AudioClip gameOverMusic;
    
    [Header("SFX Clips")]
    public AudioClip buttonClick;
    public AudioClip correctAnswer;
    public AudioClip incorrectAnswer;
    public AudioClip starCollect;
    public AudioClip levelComplete;
    public AudioClip popupOpen;
    public AudioClip popupClose;
    
    [Header("Voice Clips")]
    public AudioClip voiceWelcome;
    public AudioClip voiceCorrect;
    public AudioClip voiceTryAgain;
    public AudioClip voiceExcellent;
    public AudioClip voiceGoodJob;
    
    [Header("Audio Settings")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.8f;
    [Range(0f, 1f)] public float sfxVolume = 0.8f;
    [Range(0f, 1f)] public float voiceVolume = 1f;
    public bool musicEnabled = true;
    public bool sfxEnabled = true;
    public bool voiceEnabled = true;
    
    // Audio clip database
    private Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> voiceClips = new Dictionary<string, AudioClip>();
    
    // Music fading
    private Coroutine musicFadeCoroutine;
    
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        // Load saved settings
        LoadAudioSettings();
        
        // Start with main menu music
        PlayMusic(mainMenuMusic, true);
    }
    
    void InitializeAudio()
    {
        // Initialize SFX dictionary
        sfxClips.Add("buttonClick", buttonClick);
        sfxClips.Add("correctAnswer", correctAnswer);
        sfxClips.Add("incorrectAnswer", incorrectAnswer);
        sfxClips.Add("starCollect", starCollect);
        sfxClips.Add("levelComplete", levelComplete);
        sfxClips.Add("popupOpen", popupOpen);
        sfxClips.Add("popupClose", popupClose);
        
        // Initialize voice dictionary
        voiceClips.Add("welcome", voiceWelcome);
        voiceClips.Add("correct", voiceCorrect);
        voiceClips.Add("tryAgain", voiceTryAgain);
        voiceClips.Add("excellent", voiceExcellent);
        voiceClips.Add("goodJob", voiceGoodJob);
        
        // Create audio sources if they don't exist
        if (musicSource == null) musicSource = gameObject.AddComponent<AudioSource>();
        if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();
        if (voiceSource == null) voiceSource = gameObject.AddComponent<AudioSource>();
        if (ambientSource == null) ambientSource = gameObject.AddComponent<AudioSource>();
        
        // Configure audio sources
        ConfigureAudioSource(musicSource, true, false);  // Music: loop, no spatial
        ConfigureAudioSource(sfxSource, false, false);   // SFX: no loop, no spatial
        ConfigureAudioSource(voiceSource, false, false); // Voice: no loop, no spatial
        ConfigureAudioSource(ambientSource, true, false); // Ambient: loop, no spatial
        
        ApplyAudioSettings();
    }
    
    #region Public Music Methods
    
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (!musicEnabled || clip == null) return;
        
        // Stop any current music fade
        if (musicFadeCoroutine != null)
            StopCoroutine(musicFadeCoroutine);
        
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
        
        Debug.Log($"Playing music: {clip.name}");
    }
    
    public void StopMusic()
    {
        musicSource.Stop();
    }
    
    public void PauseMusic()
    {
        musicSource.Pause();
    }
    
    public void ResumeMusic()
    {
        if (musicEnabled)
            musicSource.UnPause();
    }
    
    public void FadeOutMusic(float duration = 1f)
    {
        if (musicFadeCoroutine != null)
            StopCoroutine(musicFadeCoroutine);
        
        musicFadeCoroutine = StartCoroutine(FadeMusicCoroutine(0f, duration));
    }
    
    public void FadeInMusic(AudioClip clip, float duration = 1f, bool loop = true)
    {
        if (musicFadeCoroutine != null)
            StopCoroutine(musicFadeCoroutine);
        
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.volume = 0f;
        musicSource.Play();
        
        musicFadeCoroutine = StartCoroutine(FadeMusicCoroutine(musicVolume, duration));
    }
    
    public void CrossFadeMusic(AudioClip newClip, float duration = 1f)
    {
        if (musicFadeCoroutine != null)
            StopCoroutine(musicFadeCoroutine);
        
        musicFadeCoroutine = StartCoroutine(CrossFadeCoroutine(newClip, duration));
    }
    
    #endregion
    
    #region Public SFX Methods
    
    public void PlaySFX(string sfxName, float volumeScale = 1f)
    {
        if (!sfxEnabled || !sfxClips.ContainsKey(sfxName)) return;
        
        AudioClip clip = sfxClips[sfxName];
        sfxSource.PlayOneShot(clip, volumeScale * sfxVolume * masterVolume);
        
        Debug.Log($"Playing SFX: {sfxName}");
    }
    
    public void PlaySFX(AudioClip clip, float volumeScale = 1f)
    {
        if (!sfxEnabled || clip == null) return;
        
        sfxSource.PlayOneShot(clip, volumeScale * sfxVolume * masterVolume);
    }
    
    public void PlayRandomSFX(AudioClip[] clips, float volumeScale = 1f)
    {
        if (!sfxEnabled || clips == null || clips.Length == 0) return;
        
        AudioClip clip = clips[Random.Range(0, clips.Length)];
        PlaySFX(clip, volumeScale);
    }
    
    #endregion
    
    #region Public Voice Methods
    
    public void PlayVoice(string voiceName, float volumeScale = 1f)
    {
        if (!voiceEnabled || !voiceClips.ContainsKey(voiceName)) return;
        
        AudioClip clip = voiceClips[voiceName];
        voiceSource.Stop(); // Stop any current voice
        voiceSource.PlayOneShot(clip, volumeScale * voiceVolume * masterVolume);
        
        Debug.Log($"Playing voice: {voiceName}");
    }
    
    public void PlayVoice(AudioClip clip, float volumeScale = 1f)
    {
        if (!voiceEnabled || clip == null) return;
        
        voiceSource.Stop();
        voiceSource.PlayOneShot(clip, volumeScale * voiceVolume * masterVolume);
    }
    
    public void StopVoice()
    {
        voiceSource.Stop();
    }
    
    #endregion
    
    #region Audio Settings Methods
    
    public void ApplyAudioSettings()
    {
        // Apply volumes with master volume
        musicSource.volume = musicVolume * masterVolume * (musicEnabled ? 1f : 0f);
        sfxSource.volume = sfxVolume * masterVolume * (sfxEnabled ? 1f : 0f);
        voiceSource.volume = voiceVolume * masterVolume * (voiceEnabled ? 1f : 0f);
        ambientSource.volume = musicVolume * masterVolume * (musicEnabled ? 1f : 0f);
        
        // Save settings
        SaveAudioSettings();
    }
    
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        ApplyAudioSettings();
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        ApplyAudioSettings();
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        ApplyAudioSettings();
    }
    
    public void SetVoiceVolume(float volume)
    {
        voiceVolume = Mathf.Clamp01(volume);
        ApplyAudioSettings();
    }
    
    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;
        ApplyAudioSettings();
    }
    
    public void ToggleSFX()
    {
        sfxEnabled = !sfxEnabled;
        ApplyAudioSettings();
    }
    
    public void ToggleVoice()
    {
        voiceEnabled = !voiceEnabled;
        ApplyAudioSettings();
    }
    
    #endregion
    
    #region Save/Load Settings
    
    public void SaveAudioSettings()
    {
        SaveLoadSystem.SaveSettings(musicVolume, sfxVolume, musicEnabled, sfxEnabled);
    }
    
    public void LoadAudioSettings()
    {
        SettingsData settings = SaveLoadSystem.LoadSettings();
        
        musicVolume = settings.musicVolume;
        sfxVolume = settings.sfxVolume;
        musicEnabled = settings.musicEnabled;
        sfxEnabled = settings.sfxEnabled;
        
        ApplyAudioSettings();
    }
    
    #endregion
    
    #region Utility Methods
    
    private void ConfigureAudioSource(AudioSource source, bool loop, bool spatial)
    {
        source.loop = loop;
        source.spatialBlend = spatial ? 1f : 0f; // 0 = 2D, 1 = 3D
        source.playOnAwake = false;
    }
    
    private IEnumerator FadeMusicCoroutine(float targetVolume, float duration)
    {
        float startVolume = musicSource.volume;
        float timer = 0f;
        
        while (timer < duration)
        {
            timer += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, timer / duration);
            yield return null;
        }
        
        musicSource.volume = targetVolume;
        
        // If fading to 0, stop the music
        if (targetVolume == 0f)
        {
            musicSource.Stop();
        }
    }
    
    private IEnumerator CrossFadeCoroutine(AudioClip newClip, float duration)
    {
        // Fade out current music
        yield return StartCoroutine(FadeMusicCoroutine(0f, duration / 2));
        
        // Switch to new clip
        musicSource.clip = newClip;
        musicSource.Play();
        
        // Fade in new music
        yield return StartCoroutine(FadeMusicCoroutine(musicVolume, duration / 2));
    }
    
    // Preload audio clips to prevent lag
    public void PreloadAudioClips()
    {
        foreach (var clip in sfxClips.Values)
        {
            if (clip != null && clip.loadState == AudioDataLoadState.Unloaded)
            {
                clip.LoadAudioData();
            }
        }
    }
    
    // Unload audio clips to free memory
    public void UnloadAudioClips()
    {
        foreach (var clip in sfxClips.Values)
        {
            if (clip != null && clip.loadState == AudioDataLoadState.Loaded)
            {
                clip.UnloadAudioData();
            }
        }
    }
    
    #endregion
    
    #region Game Event Handlers
    
    // These methods can be called from other game systems
    public void OnCorrectAnswer()
    {
        PlaySFX("correctAnswer");
        PlayVoice("correct");
    }
    
    public void OnIncorrectAnswer()
    {
        PlaySFX("incorrectAnswer");
        PlayVoice("tryAgain");
    }
    
    public void OnStarCollected()
    {
        PlaySFX("starCollect");
    }
    
    public void OnLevelComplete()
    {
        PlaySFX("levelComplete");
        PlayVoice("excellent");
        CrossFadeMusic(victoryMusic, 1f);
    }
    
    public void OnButtonClick()
    {
        PlaySFX("buttonClick");
    }
    
    #endregion
}