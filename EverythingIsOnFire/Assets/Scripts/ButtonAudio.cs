using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class ButtonAudio : MonoBehaviour
{
    public Button[] Buttons;
    public AudioSource ad;
    private void Start()
    {
        for (int i = 0; i < Buttons.Length;++i){
            TryAddListener(Buttons[i]);
        }
    
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
