using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    public void SelectLevel(int sceneIndex)
    {
        LevelManager.Instance.LoadChosenScene(sceneIndex);
    }
   
}
