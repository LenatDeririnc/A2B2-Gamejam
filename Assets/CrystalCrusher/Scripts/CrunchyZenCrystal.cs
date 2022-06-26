using System;
using UnityEngine;

public class CrunchyZenCrystal : MonoBehaviour
{
    public Rigidbody rigidbody;
    public GameObject destroyPrefab;
    public Action<bool> onHitOrMiss;

    public void AnimateDestroy()
    {
        Destroy(gameObject);
        
        if(destroyPrefab) 
            Instantiate(destroyPrefab, transform.position, transform.rotation);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        AnimateDestroy();
        onHitOrMiss?.Invoke(false);
    }
}
