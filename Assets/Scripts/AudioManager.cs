using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource spawnSound;

    [SerializeField]
    AudioSource spawnEmptySound;

    [SerializeField]
    AudioSource gateSound;

    [SerializeField]
    AudioSource countSound;

    [SerializeField]
    AudioSource recollectSound;

    AudioSource bgm;

    float volumeSpeed = 2.5f;
    float bgmEndDuration = 0f;

    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bgmEndDuration > 0)
        {
            bgm.volume = Mathf.SmoothDamp(bgm.volume, 0, ref volumeSpeed, bgmEndDuration);
            if (bgm.volume <= 0)
            {
                bgmEndDuration = 0f;
                bgm.Stop();

                Time.timeScale = 0f;
            }
        }
    }

    public void PlaySpawnSound()
    {
        spawnSound.Play();
    }

    // Play when the pool is empty (= cannot spawn object anymore)
    public void PlaySpawnEmptySound()
    {
        spawnEmptySound.Play();
    }

    public void PlayGateSound()
    {
        gateSound.Play();
    }

    public void PlayCountSound()
    {
        countSound.Play();
    }

    public void PlayRecollectSound()
    {
        recollectSound.Play();
    }

    // Gradually lower the volume of BGM (until 0) over the specified duration
    public void LowerVolumeBGMToEnd(float duration)
    {
        bgmEndDuration = duration;
    }
}
