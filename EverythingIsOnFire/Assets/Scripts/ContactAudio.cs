using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAudio : MonoBehaviour {
    AudioSource soundSource;
	public AudioClip clipSound;
	void Awake(){
		soundSource = GetComponent<AudioSource>();
		soundSource.clip = clipSound;
	}
    // Use this for initialization
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "fire") { 
        soundSource.Play();
    }
	}
	// Update is called once per frame
	
}
