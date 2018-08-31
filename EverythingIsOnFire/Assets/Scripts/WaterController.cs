using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {
    [SerializeField]
    private float _waterSurvivalTime;
    private AudioSource ad;
    public AudioClip waterDrop;
    public AudioClip splash;
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;

    IEnumerator Start()
    {
        ad = GetComponent<AudioSource>();
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        ad.pitch = randomPitch;
        ad.clip = waterDrop;
        ad.Play();
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
    void OnTriggerEnter2D(Collider2D collision)
    {
       

    }
}
