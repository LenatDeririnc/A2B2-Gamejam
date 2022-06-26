using System.Collections.Generic;
using UnityEngine;

public class HalfLifeVox : MonoBehaviour
{
    public AudioSource source;

    private readonly Queue<AudioClip> _clipQueue = new();

    public void Say(params AudioClip[] clips)
    {
        foreach (var clip in clips)
            _clipQueue.Enqueue(clip);
    }

    public bool IsSpeaking => source.isPlaying;

    public void Update()
    {
        if (!source.isPlaying && _clipQueue.TryDequeue(out var clip))
        {
            source.clip = clip;
            source.Play();
        }
    }
}
