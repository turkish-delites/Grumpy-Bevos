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
        _firePoints = new List<FirePointController>();
        var size = _mainBlockObject.gameObject.transform.localScale;
        Debug.Log(size);
        float x = Mathf.Floor(size.x);
        float y = Mathf.Floor(size.y);
        float scale_x = 1 / x;
        float scale_y = 1 / y;

        float xIncrement = 1 / (2 * x);
        float yIncrement = 1 / (2 * y);

        List<float> y_points = new List<float>();
        List<float> x_points = new List<float>();


        //create the x points
        if (x % 2 == 0)
        {
            for(int i = 0; i < x; ++i)
            {
                float xPointInitial = (-x / 2 + 0.5f) * xIncrement;
                x_points.Add(xPointInitial + i * xIncrement);
            }
        }
        else
        {
            for (int i = 0; i < x; ++i)
            {
                float xPointInitial = -((x-1)/2) * xIncrement;
                x_points.Add(xPointInitial + i * xIncrement);
            }
        }

        //create the y points
        if (y % 2 == 0)
        {
            for (int i = 0; i < y; ++i)
            {
                float yPointInitial = (-y / 2 + 0.5f) * yIncrement;
                y_points.Add(yPointInitial + i * yIncrement);
            }
        }
        else
        {
            for (int i = 0; i < y; ++i)
            {
                float yPointInitial = -((y - 1) / 2) * yIncrement;
                y_points.Add(yPointInitial + i * yIncrement);
            }
        }

        foreach (float point in x_points)
        {
            Debug.Log(point);
        }

        foreach(float xpoint in x_points)
        {
            foreach(float ypoint in y_points)
            {
                AddFirePoint(xpoint, ypoint, scale_x, scale_y);
            }
        }
    }

    private void AddFirePoint(float x, float y, float scale_x, float scale_y){
        GameObject obj = Instantiate(firePointPrefab) as GameObject;
        obj.GetComponent<FirePointController>().FirePointGroupController = this.gameObject;
        obj.transform.parent = _mainBlockObject.transform;
        obj.transform.localPosition = new Vector3(x, y, 0);
        obj.transform.localScale = new Vector3(scale_x, scale_y, 1);


        _firePoints.Add(obj.GetComponent<FirePointController>());
    }
}
