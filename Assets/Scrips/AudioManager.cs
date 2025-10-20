using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [Tooltip("Audio source for sound effects (SFX)")]
    public AudioSource sfxSource;
    [Tooltip("Audio source for background and music tracks")]
    public AudioSource musicSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f;

    [Header("Audio Clips")]
    public AudioClip[] backgroundMusic;
    public AudioClip[] gameMusic;
    public AudioClip playButtonClickSound;
    public AudioClip optionButtonClickSound;
    public AudioClip getStars;
    public AudioClip getShields;
    public AudioClip winMusic;
    public AudioClip loseMusic;
    public AudioClip getShieldDisappear;
    public AudioClip getHurt;
    public AudioClip getWood;
    public AudioClip upgradeLevel;

    // Keys for PlayerPrefs
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";

    private void Awake()
    {
        // Ensure only one AudioManager exists (Singleton pattern)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
            LoadSettings(); // Load saved user preferences
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate managers
        }
    }

    /// <summary>
    /// Loads volume settings from PlayerPrefs.
    /// </summary>
    private void LoadSettings()
    {
        sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);
        musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);

        ApplyVolumeSettings();
    }

    /// <summary>
    /// Apply current volume levels to the audio sources.
    /// </summary>
    private void ApplyVolumeSettings()
    {
        if (sfxSource != null)
            sfxSource.volume = sfxVolume;

        if (musicSource != null)
            musicSource.volume = musicVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxVolume);
        PlayerPrefs.Save();
        ApplyVolumeSettings();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, musicVolume);
        PlayerPrefs.Save();
        ApplyVolumeSettings();
    }

    public float GetSFXVolume() => sfxVolume;
    public float GetMusicVolume() => musicVolume;

    /// <summary>
    /// Plays a random SFX clip from the provided array.
    /// </summary>
    public void PlayRandomSFX(AudioClip[] audioClips)
    {
        if (audioClips == null || audioClips.Length == 0)
            return;

        int randomIndex = Random.Range(0, audioClips.Length);
        PlaySFX(audioClips[randomIndex]);
    }

    /// <summary>
    /// Plays a random music clip from the provided array.
    /// </summary>
    public void PlayRandomMusic(AudioClip[] audioClips)
    {
        if (audioClips == null || audioClips.Length == 0)
            return;

        int randomIndex = Random.Range(0, audioClips.Length);
        PlayMusic(audioClips[randomIndex]);
    }

    /// <summary>
    /// Plays a single SFX clip without interrupting other SFX.
    /// </summary>
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip, sfxVolume);
    }

    /// <summary>
    /// Starts playing background music in a loop.
    /// </summary>
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null)
            return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    /// <summary>
    /// Stops the current music track.
    /// </summary>
    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();
    }
}
