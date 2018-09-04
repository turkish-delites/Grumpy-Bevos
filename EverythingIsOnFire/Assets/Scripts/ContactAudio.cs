using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAudio : MonoBehaviour {
    AudioSource soundSource;
	public AudioClip clipSound;
	void Awake(){
		soundSource = GetComponent<AudioSource>();
		soundSource.clip = clipSound;
        soundSource.volume = 0.1f;
	}
    // Use this for initialization
    void OnCollisionEnter2D(Collision2D collision) {
        //string tagName = collision.otherCollider.gameObject.tag;
        soundSource.pitch = Random.Range(0.95f, 1.05f);
            soundSource.Play();
	}

	
}
