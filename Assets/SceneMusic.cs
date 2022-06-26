using System;
using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    private void Awake()
    {
        _source.Play();
    }
}
