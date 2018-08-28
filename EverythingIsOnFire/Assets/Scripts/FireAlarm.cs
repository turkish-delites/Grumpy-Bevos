using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FireAlarm : MonoBehaviour {

    [SerializeField]
    private List<SprinklerController> _attachedSprinklers;
    [SerializeField]
    private bool _isActivated;

    void Start()
    {
        _isActivated = false;    
    }

    public void Activate(Fire fireSource)
    {
        if(!_isActivated)
        {
            _isActivated = true;
            fireSource.ResetFireTimer();
            _attachedSprinklers.ForEach(x => x.Activate());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Fire other = collision.gameObject.GetComponent<Fire>();
        if (other != null && other.ReadyToSpread)
        {
            Activate(other);
        }
    }
}
