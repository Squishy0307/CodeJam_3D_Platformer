using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public string name;
    public AudioClip clip;
    public AudioMixerGroup AudioMixerGroup;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    [Range (0, 1f)]
    public float spatialBlend;

    
    public float Min3D_Distance;

    public bool loop;

    

    [HideInInspector]
    public AudioSource source;

}
