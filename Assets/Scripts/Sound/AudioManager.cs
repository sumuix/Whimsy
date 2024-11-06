using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioclip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        Play_Sound("Theme");
    }

    public void Play_Sound(string sound_Name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == sound_Name);
        if (s == null)
        {
            //Debug.LogWarning("Sound :" + sound_Name + " not found!");
            return;
        }

        s.source.Play();
    }
}
