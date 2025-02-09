using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Woah its the audio
    public AudioClip firstTrack;    // First audio clip to play
    public AudioClip loopTrack;     // Second audio clip to loop

    void Start()
    {
        // Start playing the first track
        audioSource.clip = firstTrack;
        audioSource.Play();
    }

    void Update()
    {
        // Check if the first track has finished playing (faster transition)
        if (audioSource.clip == firstTrack && audioSource.time >= audioSource.clip.length - 0.1f)
        {
            // Switch to the second track and loop it
            audioSource.clip = loopTrack;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
