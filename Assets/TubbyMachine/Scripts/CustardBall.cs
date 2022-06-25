using UnityEngine;

public class CustardBall : MonoBehaviour
{
    public GameObject splortPrefab;

    public Transform ballModel;
    public Rigidbody rigidbody;
    public float magnitudeStretchScale;

    private bool isDestroyed;

    private void Update()
    {
        if(rigidbody.velocity != Vector3.zero) 
            ballModel.rotation = Quaternion.LookRotation(rigidbody.velocity);

        var scale = 1f + rigidbody.velocity.magnitude * magnitudeStretchScale;
        ballModel.localScale = new Vector3(1, 1, scale);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(isDestroyed)
            return;

        isDestroyed = true;
        
        Instantiate(splortPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
