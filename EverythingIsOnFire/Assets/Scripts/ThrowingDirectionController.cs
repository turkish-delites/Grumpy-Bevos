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
    [SerializeField]
    private GameObject _projectileQueueObj;
    [SerializeField]
    private float MaxPlayerPullBack;
    private List<GameObject> _projectileQueue;

    private Vector3 _start, _end;

	void Start () {
        _start = _end = Vector3.zero;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, _start);
        _lineRenderer.SetPosition(1, _end);

        SetUpProjectileQueue();
    }

    private void PopProjectileQueue()
    {
        var first = _projectileQueue[0];
        _projectileQueue.RemoveAt(0);
        Destroy(first);
        for(int i = 0; i < _projectileQueue.Count; i++)
        {
            _projectileQueue[i].transform.position += Vector3.left;
        }
    }

    private void SetUpProjectileQueue()
    {
        _projectileQueue = new List<GameObject>();

        for (int i = 0; i < _objectsToThrow.Count; i++)
        {
            var projectileObj = Instantiate(_objectsToThrow[i]);
            projectileObj.transform.parent = _projectileQueueObj.transform;
            projectileObj.transform.localPosition = new Vector3(i,0,0);
            if(projectileObj.GetComponent<Rigidbody2D>())
            {
                Destroy(projectileObj.GetComponent<Rigidbody2D>());
            }

            if (projectileObj.GetComponent<BoxCollider2D>())
            {
                Destroy(projectileObj.GetComponent<BoxCollider2D>());
            }

            _projectileQueue.Add(projectileObj);
        }
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
            if(objectToThrow.GetComponent<LanternController>())
            {
                objectToThrow.GetComponent<LanternController>().DoNotExtinguish = false;
            }

            PopProjectileQueue();
        }
    }

    private void UpdateEnd()
    {
        var currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var prevEnd = _end;
        _end = currentMousePos;
        var mouseDelta = _end - _start;
        if(mouseDelta.magnitude > MaxPlayerPullBack)
        {
            mouseDelta = mouseDelta.normalized * MaxPlayerPullBack;
            _end = _start + mouseDelta;
        }
    }

    private void OnMouseDown()
    {
        var currentPos = transform.position;
        _lineRenderer.SetPosition(0, currentPos);
        _lineRenderer.SetPosition(1, currentPos);
        var currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _start = currentMousePos;
        _end = currentMousePos;
    }

    private void OnMouseDrag()
    {
        UpdateEnd();
        var mouseDelta = _end - _start;
        var currentPos = transform.position;

        _lineRenderer.SetPosition(1, currentPos + mouseDelta);
    }

    private void OnMouseUp()
    {
        var mouseDelta =  _start - _end;
        var dir = mouseDelta.normalized;
        var magnitude = mouseDelta.magnitude * _throwingScale;
        ThrowTopObject(dir, magnitude);
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position);
    }
}
