using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAudio : MonoBehaviour {
    AudioSource soundSource;
    //public BoxCollider2D cd;
	public AudioClip clipSound;
	void Awake(){
		soundSource = GetComponent<AudioSource>();
		soundSource.clip = clipSound;
        soundSource.volume = 0.1f;
	}
    // Use this for initialization
    void OnCollisionEnter2D(Collision2D collision) {
        string tagName = collision.otherCollider.gameObject.tag;
        if (tagName != "fire" && tagName != "water") { 
            soundSource.Play();
        }
	}
	// Update is called once per frame
	
}
