using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartButton : MonoBehaviour {
    public void RestartGame()
    {
        LevelManager.Instance.RestartGame();
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
