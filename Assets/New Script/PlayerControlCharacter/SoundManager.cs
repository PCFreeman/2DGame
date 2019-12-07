using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource BGMSource;
    public AudioClip BOSSFIGHT;
    public AudioClip Normal;
    public static SoundManager instance = null;
    // Start is called before the first frame update

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void ChangeBGM(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    public void PlayFinalBoss()
    {
        BGMSource.clip = BOSSFIGHT;
        BGMSource.Play();
    }
    public void PlayNormal()
    {
        BGMSource.clip = Normal;
        BGMSource.Play();
    }
}
