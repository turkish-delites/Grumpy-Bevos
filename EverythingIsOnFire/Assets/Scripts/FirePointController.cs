using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointController : MonoBehaviour
{
    public GameObject AttachedFire;
    public GameObject FirePrefab;
    
    // Use this for initialization
    void Start () {
        GetComponent<BoxCollider2D>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        if(AttachedFire != null)
        {
            AttachedFire.transform.position = transform.position;
        }
	}

    public void AddFire(Fire fireSource)
    {
        if(AttachedFire == null)
        {
            //Debug.Log("adding fire");
            fireSource.ResetFireTimer();
            AttachedFire = Instantiate(FirePrefab);
            AttachedFire.GetComponent<Fire>().AttachedFirePoint = this;
            AttachedFire.transform.position = transform.position;
        }
    }
    
    public void OnTriggerStay2D(Collider2D collision)
    {
        Fire other = collision.gameObject.GetComponent<Fire>();
        //Debug.Log("out : " + collision.name + " " + other + " " + name);
        if (other != null && other.ReadyToSpread)
        {
            //Debug.Log("inside : " + collision.name + " " + other + " " + name);
            AddFire(other);
        }
    }

    void OnDestroy()
    {
        if(AttachedFire != null)
        {
            Destroy(AttachedFire);
        }
    }
}
