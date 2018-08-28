using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField]
    private List<FirePointController> _firePoints;
    [SerializeField]
    private GameObject _mainBlockObject;
    [SerializeField]
    private float _piercingShotSpeedToBreak = 0f;
	// Update is called once per frame
	void Update () {
        CheckIfCompletelyOnFire();
	}

    private void CheckIfCompletelyOnFire()
    {
        if(_firePoints.TrueForAll(x => x.AttachedFire != null))
        {
            Destroy(_mainBlockObject);
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
