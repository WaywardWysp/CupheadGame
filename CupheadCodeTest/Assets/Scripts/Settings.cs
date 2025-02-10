using UnityEngine;

public class SettingsCode : MonoBehaviour
{
    public AudioClip clickSound; // The audio
    public GameObject objectToActivate;
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

        // Ensure object is hidden
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        // Play the audio when object is clicked
        if (audioSource != null && clickSound != null)
        {
            audioSource.Play();
            StartCoroutine(HandleClick());
        }
    }

    private System.Collections.IEnumerator HandleClick()
    {
        // Wait
        yield return new WaitForSeconds(0.5f);

        // Toggle visibility
        if (objectToActivate != null)
        {
            bool isCurrentlyActive = objectToActivate.activeSelf;
            objectToActivate.SetActive(!isCurrentlyActive); // Toggle visibility
        }
    }
}
