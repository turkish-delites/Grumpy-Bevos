using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FireAlarm : MonoBehaviour {

    [SerializeField]
    private List<SprinklerController> _attachedSprinklers;
    [SerializeField]
    private bool _isActivated;
    AudioSource ad;
   public AudioClip fireAlarm;
    void Start()
    {
        _isActivated = false;
        ad = GetComponent<AudioSource>();
        ad.clip = fireAlarm;
    }

    public void Activate(Fire fireSource)
    {
        if(!_isActivated)
        {
            _isActivated = true;
            ad.Play();
            fireSource.ResetFireTimer();
            _attachedSprinklers.ForEach(x => x.Activate());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Fire other = collision.gameObject.GetComponent<Fire>();
        if (other != null)
        {
            Activate(other);
        }
    }
}
