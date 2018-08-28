using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public FirePointController AttachedFirePoint;
    public bool ReadyToSpread;
    [SerializeField]
    private float _secondsToSpread;

    private void Awake()
    {
        ReadyToSpread = false;
    }

    void Start ()
    {
        StartCoroutine(TimerToSpread());
	}

    public void ResetFireTimer()
    {
        StartCoroutine(TimerToSpread());
    }

    private IEnumerator TimerToSpread()
    {
        ReadyToSpread = false;
        yield return new WaitForSeconds(_secondsToSpread);
        ReadyToSpread = true;
        Debug.Log("resetting fire");
    }
}
