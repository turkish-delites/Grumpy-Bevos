using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public bool ReadyToSpread { get; set; }
    [SerializeField]
    private float _secondsToSpread = .25f;

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
    }
}
