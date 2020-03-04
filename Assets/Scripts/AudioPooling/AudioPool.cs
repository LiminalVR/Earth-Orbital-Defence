using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    public static AudioPool Instance;

    public int InitialCount;
    public AudioSource SourcePrefab;
    public List<AudioSource> AllSources;


    public void Start()
    {
        Instance = this;

        for (var i = 0; i < InitialCount; i++)
        {
            AddAudioSource();
        }
    }


    public void PlaySound(AudioClip clipToPlay, float volume = 1f)
    {
        foreach (var source in AllSources.Where(source => !source.isPlaying))
        {
            source.volume = volume;
            source.clip = clipToPlay;
            source.Play();

            return;
        }

        AddAudioSource();
        PlaySound(clipToPlay, volume);
    }

    public void AddAudioSource()
    {
        var source = Instantiate(SourcePrefab);
        source.transform.SetParent(transform);
        source.transform.localPosition = Vector3.zero;
        source.transform.localEulerAngles = Vector3.zero;

        AllSources.Add(source);
    }

}
