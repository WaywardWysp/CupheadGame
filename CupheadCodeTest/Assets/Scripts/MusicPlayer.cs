using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; // For Dictionary

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;

    [Header("Music Tracks")]
    public AudioClip mainMenuTrack;
    public AudioClip loopTrack;
    public AudioClip lossTrack;
    public AudioClip winTrack; // New win track

    // Dictionary to store music per scene
    private Dictionary<string, AudioClip> sceneMusicMap = new Dictionary<string, AudioClip>();

    void Awake()
    {
        // Singleton to persist audio across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Set up music mapping
        sceneMusicMap["Main"] = mainMenuTrack;  // Menu music
        sceneMusicMap["Swamp"] = loopTrack;        // Loop track for Level 1
        sceneMusicMap["Swamp"] = loopTrack;        // Loop track continues for Level 2
        sceneMusicMap["loss"] = lossTrack;      // Loss scene music
        sceneMusicMap["loss"] = lossTrack;    // Another loss screen
        sceneMusicMap["win"] = winTrack;      // Win screen music

        // Play music for the current scene
        PlayMusicForCurrentScene();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForCurrentScene();
    }

    private void PlayMusicForCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneMusicMap.ContainsKey(sceneName))
        {
            PlayMusic(sceneMusicMap[sceneName], loop: true);
        }
    }

    private void PlayMusic(AudioClip clip, bool loop)
    {
        if (clip != null && audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        }
    }
}
