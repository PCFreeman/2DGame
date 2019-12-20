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
    List<AudioSource> AudioList;
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
    private void Start()
    {
        AudioList = new List<AudioSource>();
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
    public void PlaySingleNew(AudioClip clip)
    {

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;
        AudioList.Add(audioSource);
        audioSource.clip = clip;
        audioSource.Play();
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

    private void Update()
    {
        if(AudioList != null)
        {
            for(int i=0;i<AudioList.Count;i++)
            {
                if(!AudioList[i].isPlaying)
                {
                    Destroy(AudioList[i]);
                    AudioList.Remove(AudioList[i]);
                }
            }

        }
    }
}
