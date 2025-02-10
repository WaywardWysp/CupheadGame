using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public AudioClip clickSound; // The audio clip
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
        // Play the audio
        if (audioSource != null && clickSound != null)
        {
            audioSource.Play();
            StartCoroutine(ExitAfterSound());
        }
        else
        {
            ExitingGame();
        }
    }

    private System.Collections.IEnumerator ExitAfterSound()
    {
        // Wait
        yield return new WaitForSeconds(audioSource.clip.length);
        ExitingGame();
    }

    private void ExitingGame()
    {
        // Fr just close the game
        Debug.Log("Exiting Game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
