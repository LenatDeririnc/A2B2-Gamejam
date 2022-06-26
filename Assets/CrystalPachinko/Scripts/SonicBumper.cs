using UnityEngine;

public class SonicBumper : MonoBehaviour
{
    public AudioSource source;
    public Animator animator;
    public float bumpAmount;
    
    public void OnCollisionEnter(Collision other)
    {
        if(!source.isPlaying)
            source.Play();

        var center = transform.position;
        var otherCenter = other.collider.transform.position;

        var vector = (otherCenter - center).normalized * bumpAmount;
        other.rigidbody.velocity = vector * bumpAmount;
        
        animator.SetTrigger("Bump");
    }
}
