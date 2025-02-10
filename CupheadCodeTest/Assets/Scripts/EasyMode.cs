using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyModeButton : MonoBehaviour
{
    public AudioClip clickSound;
    public string sceneToLoad;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = clickSound;
    }

    void OnMouseDown()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.Play();
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    private System.Collections.IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.17f);
        GameModeManager.Instance.SetEasyMode(); // Set easy mode before switching scenes
        SceneManager.LoadScene(sceneToLoad);
    }
}
