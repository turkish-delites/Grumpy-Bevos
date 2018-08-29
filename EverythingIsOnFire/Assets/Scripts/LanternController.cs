using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour, IFireGroupController {
    public GameObject AttachedFire;

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
