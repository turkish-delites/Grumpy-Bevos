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
        var waterDirection = Random.insideUnitCircle;
        var waterObject = Instantiate(_waterPrefab);
        waterObject.transform.position = transform.position;
        waterObject.GetComponent<Rigidbody2D>().AddForce(waterDirection * _waterForce);
        yield return new WaitForSeconds(.3f);
        yield return Sprinkle();
    }
}
