using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class ButtonAudio : MonoBehaviour
{
    public Button startB;
    public Button helpB;
    public Button exitB;
    public Button backB;
    public AudioSource ad;
    private void Start()
    {
        TryAddListener(startB);
        TryAddListener(helpB);
        TryAddListener(exitB);
        TryAddListener(backB);
    }

    void TryAddListener(Button button)
    {
        if(button != null)
        {
            button.onClick.AddListener(TaskOnClick);
        }
    }

    void TaskOnClick(){
        ad.Play();
    }
  
}
