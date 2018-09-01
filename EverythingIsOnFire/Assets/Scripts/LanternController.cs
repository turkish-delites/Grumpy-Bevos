using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour, IFireGroupController {
    public GameObject AttachedFire;
    public bool DoNotExtinguish;
    [SerializeField]
    private float _secondsUntillFireExtinguished;
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(_secondsUntillFireExtinguished);
        if(!DoNotExtinguish)
        {
            Destroy(AttachedFire);
        }
    }

    public bool RemoveAllFires()
    {
        if(AttachedFire != null)
        {
            Destroy(AttachedFire);
            AttachedFire = null;
            return true;
        }

        return false;
    }

}
