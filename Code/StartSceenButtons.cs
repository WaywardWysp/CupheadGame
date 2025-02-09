using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Put this on the start screen scene, on an empty object

public class StartScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public Button hardModeButton;
    public Button easyModeButton;

    private bool isHardMode = false;
    private bool isEasyMode = false;

    private void Start()
    {
        // Loads saved preferences
        isHardMode = PlayerPrefs.GetInt("HardMode", 0) == 1;
        isEasyMode = PlayerPrefs.GetInt("EasyMode", 0) == 1;

        UpdateButtonVisuals();

        hardModeButton.onClick.AddListener(ToggleHardMode);
        easyModeButton.onClick.AddListener(ToggleEasyMode);
    }

    private void ToggleHardMode()
    {
        isHardMode = !isHardMode;
        isEasyMode = false; // Ensure easy mode is disabled

        PlayerPrefs.SetInt("HardMode", isHardMode ? 1 : 0);
        PlayerPrefs.SetInt("EasyMode", 0);
        PlayerPrefs.Save();

        UpdateButtonVisuals();
    }

    private void ToggleEasyMode()
    {
        isEasyMode = !isEasyMode;
        isHardMode = false; // Ensure hard mode is disabled

        PlayerPrefs.SetInt("EasyMode", isEasyMode ? 1 : 0);
        PlayerPrefs.SetInt("HardMode", 0);
        PlayerPrefs.Save();

        UpdateButtonVisuals();
    }

    private void UpdateButtonVisuals()
    {
        hardModeButton.GetComponent<Image>().color = isHardMode ? Color.red : Color.white;
        easyModeButton.GetComponent<Image>().color = isEasyMode ? Color.green : Color.white;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}

