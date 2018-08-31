using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockController : MonoBehaviour, IFireGroupController
{

    public List<FirePointController> _firePoints;
    [SerializeField]
    private GameObject firePointPrefab;
    [SerializeField]
    private GameObject _mainBlockObject;
    [SerializeField]
    private float _piercingShotSpeedToBreak = 0f;
    [SerializeField]
    private float _secondsFromFullBurnToBreak = 1f;

    void Start(){
        AddFirePoints();
    }

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

    private void AddFirePoints(){
        var size = _mainBlockObject.transform.localScale;
        Debug.Log(size);
        AddFirePoint(0, 0);
    }

    private void AddFirePoint(float x, float y){
        GameObject obj = Instantiate(firePointPrefab) as GameObject;
        obj.GetComponent<FirePointController>().FirePointGroupController = this.gameObject;
        obj.transform.parent = _mainBlockObject.transform;
        obj.transform.position = new Vector3(x, y, 0);
        _firePoints.Add(obj.GetComponent<FirePointController>());
    }
}
