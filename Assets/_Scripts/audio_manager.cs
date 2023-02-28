using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class audio_manager : MonoBehaviour
{
    public static audio_manager Instance;

    private AudioSource thisAudioSource;
    [SerializeField] AudioClip[] bgMusics;
    public string currentBgMusic;

    public Sound[] sounds;
    public bool bgMusicPlaying = false;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        else
        {
            Destroy(this.gameObject);
        }

        thisAudioSource = gameObject.GetComponent<AudioSource>();
        //currentBgMusic = thisAudioSource.clip.name;

    }

    void Start()
    {

        foreach (Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
            s.source.minDistance = s.Min3D_Distance;
            s.source.outputAudioMixerGroup = s.AudioMixerGroup;

        }        

    }

    public void Play(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Play();

    }

    public void changePitch(string name, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = pitch;
    }

    public void changeMusic(string musicName,float fadeOutDuration, float fadeInDuration)
    {
        currentBgMusic = musicName;

        StartCoroutine(setMusicTo(fadeOutDuration));

        Sequence audioChangeSeq = DOTween.Sequence();
        audioChangeSeq.Append(thisAudioSource.DOFade(0, fadeOutDuration)).SetUpdate(true);
        audioChangeSeq.Append(thisAudioSource.DOFade(0.2f, fadeInDuration)); //0.55
    }

    public void changeVolume(float volume, float duration)
    {
        thisAudioSource.DOFade(volume, duration);
    }

    IEnumerator setMusicTo(float waitTimeToChangeMusic)
    {
        yield return new WaitForSecondsRealtime(waitTimeToChangeMusic);

        if (currentBgMusic != null)
        {
            thisAudioSource.clip = Array.Find(bgMusics, AudioClip => AudioClip.name == currentBgMusic);
        }

        if (!thisAudioSource.isPlaying)
        {
            thisAudioSource.Play();
        }
    }


}
