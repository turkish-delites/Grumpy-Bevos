using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }
    [SerializeField]
    public List<string> SceneNames;
    [SerializeField]
    private int _currentSceneIndex;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        _currentSceneIndex = 0;
        DontDestroyOnLoad(gameObject);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneNames[_currentSceneIndex]);
    }

    public void LoadChosenScene(int sceneIndex)
    {
        _currentSceneIndex = sceneIndex;
        SceneManager.LoadScene(SceneNames[sceneIndex]);
    }

    public void NextScene()
    {
        _currentSceneIndex++;
        _currentSceneIndex %= SceneNames.Count;
        SceneManager.LoadScene(SceneNames[_currentSceneIndex]);
    }

    public void MainMenu()
    {
        _currentSceneIndex = 0;
        SceneManager.LoadScene(SceneNames[_currentSceneIndex]);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
