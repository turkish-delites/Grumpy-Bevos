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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tagName = collision.otherCollider.gameObject.tag; Debug.Log(tagName);
        if (breakOnce){
            if((tagName == "wood" || tagName == "metal" || tagName == "fire")){
                ad.Play();
                breakOnce = false;

               
            }
           
        }
    }
}
