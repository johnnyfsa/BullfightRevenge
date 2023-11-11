using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //this class needs to be a singleton
    private static AudioManager instance;

    public static AudioManager Instance { get; private set; }
    [SerializeField] Sound[] musicSounds, sfxSounds;
    [SerializeField] AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic(SoundType.MainTheme);
    }

    public void PlayMusic(SoundType type)
    {
        Sound s = Array.Find(musicSounds, x => x.type == type);
        if (s == null)
        {
            print("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();

        }
    }

    public void PlaySFX(SoundType type)
    {
        Sound s = Array.Find(sfxSounds, x => x.type == type);
        if (s == null)
        {
            print("Sound Not Found");

        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
            Debug.Log("Playing SFX: " + s.type);
        }
    }
}
