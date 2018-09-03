using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void StartLevel()
    {
        LevelManager.Instance.NextScene();
    }

    public void Quit()
    {
        LevelManager.Instance.Quit();
    }
}

