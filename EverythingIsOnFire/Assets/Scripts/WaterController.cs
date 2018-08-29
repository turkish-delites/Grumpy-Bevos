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
        var fireGroup = other.GetComponent<IFireGroupController>();
        if (fireGroup != null)
        {
            bool hasRemovedAFire = fireGroup.RemoveAllFires();
            if(hasRemovedAFire)
            {
                Destroy(gameObject);
            }
        }
    }
}
