using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactFireBallAudio : MonoBehaviour {
    public AudioSource ad;
    public AudioClip malotov;
    private bool breakOnce = true;
	// Use this for initialization
	void Start () {
        ad.clip = malotov;
        ad.volume = 0.2f;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(breakOnce){
            if((collision.tag == "wood" || collision.tag == "metal" || collision.tag == "fire")){
                ad.Play();
                breakOnce = false;
               // Debug.Log("broke");
               
            }
           
        }
    }
}
