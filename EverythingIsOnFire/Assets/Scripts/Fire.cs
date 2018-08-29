using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public FirePointController AttachedFirePoint;
    public bool ReadyToSpread;
    public float SecondsToSpread;

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
        yield return new WaitForSeconds(SecondsToSpread);
        ReadyToSpread = true;
        Debug.Log("resetting fire");
    }
}
