using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDirectionController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _objectsToThrow;
    private LineRenderer _lineRenderer;
    [SerializeField]
    private float _throwingScale = 100;
	// Use this for initialization
	void Start () {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.zero);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ThrowTopObject(Vector3 direction, float force)
    {
        if(_objectsToThrow.Count != 0)
        {
            var topObject = _objectsToThrow[0];
            _objectsToThrow.RemoveAt(0);
            var objectToThrow = Instantiate(topObject);
            objectToThrow.transform.position = transform.position;
            objectToThrow.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
    }

    private void OnMouseDown()
    {
        var currentPos = transform.position;
        _lineRenderer.SetPosition(0, currentPos);
        _lineRenderer.SetPosition(1, currentPos);
    }

    private void OnMouseDrag()
    {
        var currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPos -= new Vector3(0, 0, -1);
        _lineRenderer.SetPosition(1, currentPos);
    }

    private void OnMouseUp()
    {
        var start = _lineRenderer.GetPosition(0);
        var end = _lineRenderer.GetPosition(1);
        ThrowTopObject((end - start).normalized, (end - start).magnitude * _throwingScale);
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position);
    }
}
