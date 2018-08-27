using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointController : MonoBehaviour {
    public GameObject AttachedFire;
    public GameObject FirePrefab;

    [SerializeField]
    private bool _firePresent;

    // Use this for initialization
    void Start () {
        GetComponent<BoxCollider2D>().enabled = true;
        _firePresent = false;
        StartCoroutine(WaitForFire());
    }
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator WaitForFire()
    {
        if(_firePresent)
        {
            AddFire();
        }

        yield return new WaitForSecondsRealtime(.25f);
        yield return WaitForFire();
    }

    public void AddFire()
    {
        if(AttachedFire == null)
        {
            AttachedFire = Instantiate(FirePrefab);
            AttachedFire.transform.position = transform.position;
            AttachedFire.transform.parent = transform;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Fire other = collision.gameObject.GetComponent<Fire>();
        if(other != null)
        {
            _firePresent = true;
        }
    }
}
