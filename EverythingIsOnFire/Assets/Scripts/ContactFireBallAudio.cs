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
        ad.volume = 0.5f;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tagName = collision.otherCollider.gameObject.tag; Debug.Log(tagName);
        var force = collision.relativeVelocity * collision.otherRigidbody.mass;
        Debug.Log(force);
        if (breakOnce){
            if (force.magnitude > 5.0f)
            {
                ad.Play();
                breakOnce = false;
            }
               

           
        }
    }
}
