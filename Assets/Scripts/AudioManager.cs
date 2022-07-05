using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource spawnSound;

    [SerializeField]
    AudioSource gateSound;

    [SerializeField]
    AudioSource countSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlaySpawnSound()
    {
        spawnSound.Play();
    }

    public void PlayGateSound()
    {
        gateSound.Play();
    }

    public void PlayCountSound()
    {
        countSound.Play();
    }
}
