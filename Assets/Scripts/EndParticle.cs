using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndParticle : MonoBehaviour
{
    public OptionsManager optionsManager;
    public GameObject particle;
    private AudioSource audioSource;
    public float life;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        optionsManager = GameObject.Find("Options Menu Canvas").GetComponent<OptionsManager>();
        audioSource.volume = optionsManager.GetSFXVol();
        Invoke("Delete", life);
    }


    void Delete()
    {
        Destroy(particle);
    }
}
