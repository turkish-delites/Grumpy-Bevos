using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointController : MonoBehaviour {
    public GameObject AttachedFire;
    public GameObject FirePrefab;

    [SerializeField]
    private bool FirePresent;

    // Use this for initialization
    void Start () {
        GetComponent<BoxCollider2D>().enabled = true;
        FirePresent = false;
        StartCoroutine(WaitForFire());
    }
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator WaitForFire()
    {
        if(FirePresent)
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
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Fire other = collision.gameObject.GetComponent<Fire>();
        if(other != null)
        {
            FirePresent = true;
        }
    }
}
