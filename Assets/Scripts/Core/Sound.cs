using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound 
{
    public string soundName;
    public AudioClip clip;

    public bool loop;

    [Range(0.0f, 1.0f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
}
