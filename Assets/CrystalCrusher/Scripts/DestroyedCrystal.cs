using UnityEngine;
using Random = UnityEngine.Random;

public class DestroyedCrystal : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] clips;

    private void Start()
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
        
        Destroy(gameObject, 1.5f);
    }
}
