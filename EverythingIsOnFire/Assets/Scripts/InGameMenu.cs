using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InGameMenu : MonoBehaviour {
    public void MainMenu()
    {
        LevelManager.Instance.MainMenu();
    }

    public void RestartLevel()
    {
        LevelManager.Instance.ReloadScene();
    }

    public void SkipLevel()
    {
        LevelManager.Instance.NextScene();
    }

    public void Quit()
    {
        LevelManager.Instance.Quit();
    }
}
