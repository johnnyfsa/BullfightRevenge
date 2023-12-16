using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
        GetMusicAccordingToScene(SceneManager.GetActiveScene());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetMusicAccordingToScene(scene);
    }

    public void GetMusicAccordingToScene(Scene scene)
    {
        if (scene != null)
        {
            switch (scene.name)
            {
                case "Cover":
                    PlayMusic(SoundType.Title);
                    break;
                case "Main":
                    PlayMusic(SoundType.MainTheme);
                    break;
            }
        }
    }

    public void PlayMusic(SoundType type)
    {
        Sound s = Array.Find(musicSounds, x => x.type == type);
        if (s == null)
        {
            print("Music Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();

        }
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
    }


    public float GetMusicVolume()
    {
        return musicSource.volume;
    }


    public void SetSFXVolume(float value)
    {
        sfxSource.volume = value;
    }

    public float GetSFXVolume()
    {
        return sfxSource.volume;
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
        }
    }
}
