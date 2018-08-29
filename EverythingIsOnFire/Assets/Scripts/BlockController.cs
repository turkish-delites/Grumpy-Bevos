using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockController : MonoBehaviour, IFireGroupController
{
    [SerializeField]
    private List<FirePointController> _firePoints;
    [SerializeField]
    private GameObject _mainBlockObject;
    [SerializeField]
    private float _piercingShotSpeedToBreak = 0f;
    [SerializeField]
    private float _secondsFromFullBurnToBreak = 1f;

    // Update is called once per frame
    void Update () {
        StartCoroutine(CheckIfCompletelyOnFire());
	}

    public bool RemoveAllFires()
    {
        Debug.Log("removing fires");
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

    private IEnumerator CheckIfCompletelyOnFire()
    {
        if(_firePoints.TrueForAll(x => x.AttachedFire != null))
        {
            yield return new WaitForSeconds(_secondsFromFullBurnToBreak);
            //Destroy(_mainBlockObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PiercingShot>() != null &&
            collision.GetComponent<Rigidbody2D>().velocity.magnitude > _piercingShotSpeedToBreak)
        {
            collision.GetComponent<Rigidbody2D>().velocity = collision.GetComponent<Rigidbody2D>().velocity * .5f;
            Destroy(_mainBlockObject);
        }
    }
}
