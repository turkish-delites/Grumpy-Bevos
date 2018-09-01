using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }
    [SerializeField]
    private List<string> _sceneNames;

    private int _currentSceneIndex;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _currentSceneIndex = 0;
        DontDestroyOnLoad(gameObject);
    }

    public void NextScene()
    {
        _currentSceneIndex++;
        _currentSceneIndex %= _sceneNames.Count;
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
