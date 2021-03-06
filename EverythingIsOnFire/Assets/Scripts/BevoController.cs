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
    [SerializeField]
    private GameObject _mainObject;
  

    // Use this for initialization
  

    // Update is called once per frame
    void Update()
    {
       StartCoroutine(TryToBurn());

    }

 
    void OnCollisionEnter2D (Collision2D col)
    {
        var force = col.relativeVelocity * col.otherRigidbody.mass; 
        string tagName = col.otherCollider.gameObject.tag;
        if (force.magnitude > _forceToKill)
        {
           
            die();
        }

        if (tagName == "fire" && tagName == "FireBall")
        {
           
            Debug.Log("Here");
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
        Destroy(_mainObject);
    }
}

