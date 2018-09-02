using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }
    [SerializeField]
    private List<string> _sceneNames;
    [SerializeField]
    private int _currentSceneIndex;

    private void Awake()
    {
        Debug.Log("awake " + name);
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

    public void RestartGame()
    {
        _currentSceneIndex = 0;
        SceneManager.LoadScene(_sceneNames[_currentSceneIndex]);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(_sceneNames[_currentSceneIndex]);
    }

    public void NextScene()
    {
        _currentSceneIndex++;
        _currentSceneIndex %= _sceneNames.Count;
        SceneManager.LoadScene(_sceneNames[_currentSceneIndex]);
    }

    public void PreviousScene()
    {
        _currentSceneIndex--;
        //_currentSceneIndex %= _sceneNames.Count;
        SceneManager.LoadScene(_sceneNames[_currentSceneIndex]);
    }

    public void MainMenu()
    {
        _currentSceneIndex = 0;
        SceneManager.LoadScene(_sceneNames[_currentSceneIndex]);
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
