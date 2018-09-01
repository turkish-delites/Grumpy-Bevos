using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerController : MonoBehaviour {
    [SerializeField]
    private WaterController _waterPrefab;
    [SerializeField]
    private float _waterForce = 10;
    [SerializeField]
    private bool _activated = false;
    [SerializeField]
    private float _secondsBetweenDrops = .3f;

	public void Activate()
    {
        if (!_activated)
        {
            _activated = true;
            StartCoroutine(Sprinkle());
        }
    }

    private IEnumerator Sprinkle()
    {
        var waterDirection = Random.onUnitSphere;
        waterDirection.y = Mathf.Abs(waterDirection.y);
        waterDirection.z = 0;
        Debug.Log(waterDirection);
        waterDirection = new Vector2(waterDirection.x/2, waterDirection.y*2);
        Debug.Log(waterDirection);
        var waterObject = Instantiate(_waterPrefab);
        waterObject.transform.position = transform.position;
        waterObject.GetComponent<Rigidbody2D>().AddForce(waterDirection * _waterForce);
        yield return new WaitForSeconds(_secondsBetweenDrops);
        yield return Sprinkle();
    }
}
