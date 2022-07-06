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

    AudioSource bgm;

    float bgmEndDuration = 0f;

    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(bgmEndDuration);
        if (bgmEndDuration > 0)
        {
            bgmEndDuration -= Time.deltaTime;

            // float volume = bgm.volume;
            // volume = Mathf.MoveTowards(volume, 0, Time.deltaTime);

            // Debug.Log(volume);

            Debug.Log(bgmEndDuration);
        }
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

    // Gradually lower the volume of BGM (until 0) over the specified duration
    public void LowerVolumeBGMToEnd(float duration)
    {
        bgmEndDuration = duration;
    }
}
