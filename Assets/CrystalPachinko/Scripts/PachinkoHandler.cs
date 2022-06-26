using UnityEngine;

public class PachinkoHandler : MonoBehaviour
{
    public PachinkoPissShitMixer mixer;
    public Animator animator;
    
    private void OnMouseDown()
    {
        if (mixer.DropCrystal())
        {
            animator.SetTrigger("Draw");
        }
    }

    private void OnMouseEnter()
    {
        animator.SetBool("Hover", true);
    }

    private void OnMouseExit()
    {
        animator.SetBool("Hover", false);
    }
}
