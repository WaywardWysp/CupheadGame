using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public AudioClip clickSound; // The audio clip to play
    public string sceneToLoad;  // Name of the scene to load
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the audio
        audioSource.playOnAwake = false;
        audioSource.clip = clickSound;
    }

    void OnMouseDown()
    {
        // Play audio cue
        if (audioSource != null && clickSound != null)
        {
            audioSource.Play();
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    private System.Collections.IEnumerator LoadSceneAfterDelay()
    {
        // Wait
        yield return new WaitForSeconds(0.17f);

        // Disable both hard and easy mode
        PlayerPrefs.SetInt("EasyMode", 0);
        PlayerPrefs.SetInt("HardMode", 0);
        PlayerPrefs.Save();

        // Load scene
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
