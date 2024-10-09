using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour


{
    [Header("-------------- Audio Source --------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("-------------- Audio Clip --------")]
    public AudioClip Background;
    public AudioClip death;
    public AudioClip checkpoint;
    public AudioClip pause;
    public AudioClip BossFight;
    public AudioClip victory;

    private void Start()
    {
        musicSource.clip = Background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    
    
}