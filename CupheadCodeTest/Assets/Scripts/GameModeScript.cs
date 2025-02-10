using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance;

    private void Awake()
    {
        // Ensure only one instance exists (Singleton Pattern)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this manager across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void SetEasyMode()
    {
        PlayerPrefs.SetInt("EasyMode", 1);
        PlayerPrefs.SetInt("HardMode", 0);
        PlayerPrefs.Save();
    }

    public void SetHardMode()
    {
        PlayerPrefs.SetInt("EasyMode", 0);
        PlayerPrefs.SetInt("HardMode", 1);
        PlayerPrefs.Save();
    }

    public bool IsEasyMode()
    {
        return PlayerPrefs.GetInt("EasyMode", 0) == 1;
    }

    public bool IsHardMode()
    {
        return PlayerPrefs.GetInt("HardMode", 0) == 1;
    }
}


