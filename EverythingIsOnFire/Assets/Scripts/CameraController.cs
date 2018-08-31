using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private static CameraController _instance;
    public static CameraController Instance { get { return _instance; } }

    public GameObject _objectBeingTracked;
    private Vector3 _startingPosition;
    
    void Start()
    {
        _instance = this;
        _startingPosition = transform.position;
    }

    void Update()
    {
        var currentObjectPosition = _objectBeingTracked.transform.position;
        transform.position = new Vector3(currentObjectPosition.x, currentObjectPosition.y, _startingPosition.z);
        
    }

    public void TrackObject(GameObject obj)
    {
        _objectBeingTracked = obj;
    }
}
