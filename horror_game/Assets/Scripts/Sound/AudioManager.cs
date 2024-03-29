using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public string currentMusic;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
        }
    }

    private void Start()
    {
        PlayMusic(currentMusic);
    }

    public void onDestroy()
    {
        StopMusic(currentMusic);
    }
    
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        
        if(s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    
    public void StopMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        
        if(s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Stop();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        Debug.Log(sfxSounds[0].name);
        if(s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.clip = s.clip;
            if (!sfxSource.isPlaying)
            {
                
                sfxSource.PlayOneShot(s.clip);
            } 
            else if (sfxSource.isPlaying)
            {
                sfxSource.Stop();
                
                sfxSource.PlayOneShot(s.clip);
            }
        }
    }

    //Toggle Music and SFX
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}
