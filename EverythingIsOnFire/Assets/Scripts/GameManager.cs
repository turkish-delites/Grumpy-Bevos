using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour {

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckIfAllBevoDead();

    }

    private void CheckIfAllBevoDead()
    {
        var aliveBevos = GameObject.FindGameObjectsWithTag("Bevo");
        //Debug.Log(aliveBevos.Count());
        if(aliveBevos.Count() == 0)
        {
            Debug.Log("changing scene");
            LevelManager.Instance.NextScene();
        }
    }
}
