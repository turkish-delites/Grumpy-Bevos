using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointController : MonoBehaviour, IFireGroupController
{
    public GameObject AttachedFire;
    public GameObject FirePrefab;
    public GameObject FirePointGroupController;
    public bool ReadyToBeSetOnFire;

    [SerializeField]
    private float _secondsToWaitToReigniteOnceFirePutOut = 1;
    [SerializeField]
    private float _secondsForFireToWaitBeforeSpreading = 1;
    [SerializeField]
    private bool _canBeSetOnFire;
    // Use this for initialization
    void Start () {
        ReadyToBeSetOnFire = true;
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
        if(AttachedFire == null && ReadyToBeSetOnFire && _canBeSetOnFire)
        {
            //Debug.Log("adding fire");
            fireSource.ResetFireTimer();
            AttachedFire = Instantiate(FirePrefab);
            AttachedFire.GetComponent<Fire>().AttachedFirePoint = this;
            AttachedFire.transform.position = transform.position;
            AttachedFire.GetComponent<Fire>().SecondsToSpread = _secondsForFireToWaitBeforeSpreading;
        }
    }
    
    public void OnTriggerStay2D(Collider2D collision)
    {
        Fire other = collision.gameObject.GetComponent<Fire>();

        //Debug.Log("out : " + collision.name + " " + other + " " + name + " " + (other != null && other.ReadyToSpread));
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

    public bool RemoveAllFires()
    {
        return FirePointGroupController.GetComponent<IFireGroupController>().RemoveAllFires();
    }

    public bool RemoveFire()
    {
        if (AttachedFire != null)
        {
            Destroy(AttachedFire);
            AttachedFire = null;
            return true;
        }

        StartCoroutine(RemovedFireCoolDown());

        return false;
    }

    private IEnumerator RemovedFireCoolDown()
    {
        ReadyToBeSetOnFire = false;
        yield return new WaitForSeconds(_secondsToWaitToReigniteOnceFirePutOut);
        ReadyToBeSetOnFire = true;
    }
}
