using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BevoAudio : MonoBehaviour
{
    AudioSource soundSource;
    public AudioClip clipSound;
    bool audioOn = false;
    void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        soundSource.clip = clipSound;
        soundSource.volume = 0.2f;
    }
    // Use this for initialization
    void OnTriggerEnter2D(Collider2D other)
    {

        if ((other.tag == "fire" || other.tag == "fireBall") && soundSource.isPlaying == false)
        {
            soundSource.mute = false;
            soundSource.Play();
        }
        if(other.tag == "water"){
            soundSource.mute = true;
        }
    }
}
