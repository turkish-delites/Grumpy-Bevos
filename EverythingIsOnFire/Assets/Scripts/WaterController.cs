using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {
    [SerializeField]
    private float _waterSurvivalTime;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(_waterSurvivalTime);
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Fire fire = other.GetComponent<Fire>();
        if (fire != null)
        {
            if(fire.AttachedFirePoint != null)
            {
                fire.AttachedFirePoint.AttachedFire = null;
            }

            Destroy(fire.gameObject);
            Destroy(gameObject);
        }
    }
}
