using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSussing : MonoBehaviour
{
    public AudioSource source;

    public void Sus()
    {
        source.Play();
    }
}
