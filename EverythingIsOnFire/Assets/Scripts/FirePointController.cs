using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointController : MonoBehaviour
{
    public GameObject AttachedFire;
    public GameObject FirePrefab;

    [SerializeField]
    private bool _firePresent;
    
    // Use this for initialization
    void Start () {
        GetComponent<BoxCollider2D>().enabled = true;
        _firePresent = false;
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
            fireSource.ResetFireTimer();
            AttachedFire = Instantiate(FirePrefab);
            AttachedFire.transform.position = transform.position;
        }
    }
    
    public void OnTriggerStay2D(Collider2D collision)
    {
        Fire other = collision.gameObject.GetComponent<Fire>();
        if(other != null && other.ReadyToSpread)
        {
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
