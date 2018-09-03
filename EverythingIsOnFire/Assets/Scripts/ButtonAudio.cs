using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour
{
    public Button startB;
    public Button helpB;
    public Button exitB;
    public Button backB;
    public AudioSource ad;
    private void Start()
    {
        startB.onClick.AddListener(TaskOnClick);
        helpB.onClick.AddListener(TaskOnClick);
        exitB.onClick.AddListener(TaskOnClick);
        backB.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick(){
        ad.Play();
    }
  
}
