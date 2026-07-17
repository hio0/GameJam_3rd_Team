using System;
using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    [SerializeField] public AudioSource BGM;
    [SerializeField] public AudioSource clockTickSFX;
    
    
    float Rate = 0.1f;
    
    
    public IEnumerator FadeOutVol(AudioSource AudioSample)
    {
        
        while (AudioSample.volume > 0)
        {
            AudioSample.volume -= Rate * Time.deltaTime;
            yield return null;
        }
    }
    public IEnumerator FadeInVol(AudioSource AudioSample) 
    {
        while (AudioSample.volume < 1)
        {
            AudioSample.volume += Rate * Time.deltaTime;
            yield return null;
        }
    }
    void Awake()
    {
        if (soundManager != null)
        {
            Destroy(gameObject);
            
        }
        else {
        soundManager = this;
        }
        DontDestroyOnLoad(gameObject);
        soundManager.BGM.Play();
        soundManager.StartCoroutine(FadeInVol(BGM));

    }
    void Update()
    {
        
    }
}