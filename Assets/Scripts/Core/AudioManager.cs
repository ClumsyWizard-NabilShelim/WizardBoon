using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
            sound.source.volume = sound.volume;
        }
        PlayAudio("Theme");
    }

    public void PlayAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        s.source.Play();
    }

    public void StopAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        s.source.Stop();
    }
}
