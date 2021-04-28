using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager 
{   
    private static readonly AudioManager _instance = new AudioManager();
    static AudioManager(){}
    private AudioManager(){}
    public static AudioManager Instance
    {
        get { return _instance;}
    }

    public AudioMixer mainMixer;
    public AudioData audioData;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public void Init()
    {
        mainMixer = Resources.Load<AudioMixer>("Audio/MainMixer");
        audioData = Resources.Load<AudioData>("ScriptableObjects/AudioScriptableObject");
        musicSource = GameObject.FindWithTag("MainCamera").GetComponents<AudioSource>()[0];
        sfxSource = GameObject.FindWithTag("MainCamera").GetComponents<AudioSource>()[1];
        musicSource.clip = audioData.mainMenuMusic.clip;
        musicSource.Play();
    }
}
