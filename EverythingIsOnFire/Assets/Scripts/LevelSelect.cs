using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    [SerializeField]
    private GameObject _sceneButtonPrefab;

    void Start()
    {
        List<string> levels = LevelManager.Instance.SceneNames;

        for(int i = 1; i < levels.Count; i++)
        {
            var child = Instantiate(_sceneButtonPrefab, transform, false);
            child.GetComponentInChildren<Text>().text = levels[i];
            int temp = i;
            child.GetComponent<Button>().onClick.AddListener(() => SelectLevel(temp));
        }
    }

    public void SelectLevel(int sceneIndex)
    {
        LevelManager.Instance.LoadChosenScene(sceneIndex);
    }
   
}
