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
    private float _forceToKill = 10f;
    [SerializeField]
    private float _secondsFromFullBurnToBreak = 1f;
    [SerializeField]
    private bool _generateFirePoints;

    private Coroutine _destroyAfterTimerCoroutine;

    void Start()
    {
        if(_generateFirePoints)
        {
            AddFirePoints();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (_destroyAfterTimerCoroutine == null && _firePoints.TrueForAll(x => x.AttachedFire != null))
        {
            _destroyAfterTimerCoroutine = StartCoroutine(DestroyAfterTimer());
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

        InterruptFireDestruction();
        return hasRemovedAFire;
    }

    private void InterruptFireDestruction()
    {
        if(_destroyAfterTimerCoroutine != null)
        {
            StopCoroutine(_destroyAfterTimerCoroutine);
            _destroyAfterTimerCoroutine = null;
        }
    }

    private IEnumerator DestroyAfterTimer()
    {
        //Debug.Log("here");
        yield return new WaitForSeconds(_secondsFromFullBurnToBreak);
        Destroy(_mainBlockObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var force = collision.relativeVelocity * collision.otherRigidbody.mass;
        
        if (force.magnitude > _forceToKill)
        {
            Debug.Log(force.magnitude + name + _forceToKill);
            collision.otherRigidbody.velocity = collision.otherRigidbody.velocity * .5f;
            Destroy(_mainBlockObject);
        }
    }

    private void AddFirePoints(){
        _firePoints = new List<FirePointController>();
        var size = _mainBlockObject.gameObject.transform.localScale;
        //Debug.Log(size);
        float x = Mathf.Floor(size.x);
        float y = Mathf.Floor(size.y);
        float scale_x = 1 / x;
        float scale_y = 1 / y;

        float xIncrement = 1 / (2 * x);
        float yIncrement = 1 / (2 * y);

        List<float> y_points = new List<float>();
        List<float> x_points = new List<float>();
        
        //create the x points
        if ((int)x % 2 == 0)
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
        if ((int)y % 2 == 0)
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

        //add the firepoints at all the cross sections
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
