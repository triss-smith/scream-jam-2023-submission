using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

 
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound  => sound.name == name);
        s.source.Play();

        /*To implement this into other scripts to play any audio in this list, use this:
          FindObjectOfType<AudioManager>().Play("NameOfAudioFile"); */
    }
}
