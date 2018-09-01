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

    private Vector3 _mouseDelta, _prevMousePoint;

	void Start () {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.zero);

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

            var RBs = projectileObj.GetComponents<Rigidbody2D>().ToList();
            RBs.AddRange(projectileObj.GetComponentsInChildren<Rigidbody2D>());
            RBs.ForEach(x => Destroy(x));

            var colliders = projectileObj.GetComponents<Collider2D>().ToList();
            colliders.AddRange(projectileObj.GetComponentsInChildren<Collider2D>());
            colliders.ForEach(x => Destroy(x));

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

    private void UpdateDelta()
    {
        var currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _mouseDelta += currentMousePos - _prevMousePoint;

        _prevMousePoint = currentMousePos;
        if(_mouseDelta.magnitude > MaxPlayerPullBack)
        {
            _mouseDelta = _mouseDelta.normalized * MaxPlayerPullBack;
        }
        
    }

    private void OnMouseDown()
    {
        var currentPos = transform.position;
        _lineRenderer.SetPosition(0, currentPos);
        _lineRenderer.SetPosition(1, currentPos);
        var currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mouseDelta = Vector3.zero;
    }

    private void OnMouseDrag()
    {
        UpdateDelta();
        var currentPos = transform.position;

        _lineRenderer.SetPosition(1, currentPos + _mouseDelta);
    }

    private void OnMouseUp()
    {
        UpdateDelta();
        _mouseDelta = -_mouseDelta;
        var dir = _mouseDelta.normalized;
        var magnitude = _mouseDelta.magnitude * _throwingScale;
        ThrowTopObject(dir, magnitude);
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position);
    }
}
