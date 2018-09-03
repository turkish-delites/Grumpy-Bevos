using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAudio : MonoBehaviour {
    AudioSource soundSource;
	public AudioClip clipSound;
	void Awake(){
		soundSource = GetComponent<AudioSource>();
		soundSource.clip = clipSound;
        soundSource.volume = 0.05f;
	}
    // Use this for initialization
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "fire" && other.tag != "water") { 
            soundSource.Play();
        }
	}
	// Update is called once per frame
	
}
