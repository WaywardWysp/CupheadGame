using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class winloss : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("main menu");
    }
}