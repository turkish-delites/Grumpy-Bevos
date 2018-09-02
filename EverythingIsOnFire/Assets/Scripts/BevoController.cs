﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BevoController : MonoBehaviour, IFireGroupController {

	[SerializeField]
    private List<FirePointController> _firePoints;
    [SerializeField]
    private float _secondsToBurnToDeath = 2f;
    [SerializeField]
    private float _forceToKill = 5f;
    AudioSource ad1;
    public AudioSource ad2;
    public AudioClip moo;
    public AudioClip mooPain;
    private bool resetBevo = true;

    // Use this for initialization
    void Start () {
		ad1 = GetComponent<AudioSource>();
        ad1.clip = moo;
        ad1.Play();
        ad2.clip = mooPain;
	}

    // Update is called once per frame
    void Update()
    {
       StartCoroutine(TryToBurn());
       
    }

 
    void OnCollisionEnter2D (Collision2D col)
    {
        var force = col.relativeVelocity * col.otherRigidbody.mass;
        if (force.magnitude > _forceToKill)
        {
            ad2.Play();
            die();
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if (resetBevo)
        {
            if (other.tag == "fire")
            {
                ad2.loop = true;
                ad2.Play();
                resetBevo = false;
            }
        }

    }

     public bool RemoveAllFires()
    {
        //Debug.Log("removing fires");
        bool hasRemovedAFire = false;
        foreach(var firePoint in _firePoints)
        {
            bool hasRemovedThisFire = firePoint.RemoveFire();
            if(hasRemovedThisFire)
            {
                resetBevo = true;
                ad2.loop = false;
                ad2.Stop();
                hasRemovedAFire = hasRemovedThisFire;
            }
        }

        return hasRemovedAFire;
    }

  	private IEnumerator TryToBurn(){
  		if (CheckIfCompletelyOnFire()){
        
  			yield return new WaitForSeconds(_secondsToBurnToDeath);
  			if (CheckIfCompletelyOnFire()){
                die();
  			}
  		}
  	}
    
    private bool CheckIfCompletelyOnFire()
    {
        return (_firePoints.TrueForAll(x => x.AttachedFire != null));
    }

    void die()
    {   
        //here would be a good place for some kind of death animation
        Destroy(gameObject);
    }
}

