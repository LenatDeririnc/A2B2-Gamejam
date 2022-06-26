using UnityEngine;

public class DrCoomer : MonoBehaviour
{
    public AudioSource source;

    public AudioClip[] sequentailMissSounds;
    public AudioClip[] randomMissSounds;
    
    public int currentSound;

    public void OnMiss()
    {
        source.clip = currentSound < sequentailMissSounds.Length 
            ? sequentailMissSounds[currentSound++] 
            : randomMissSounds[Random.Range(0, randomMissSounds.Length)];
        source.Play();
    }
}
