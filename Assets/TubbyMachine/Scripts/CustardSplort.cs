using UnityEngine;
using Random = UnityEngine.Random;

public class CustardSplort : MonoBehaviour
{
    public Transform modelTransform;
    public Rigidbody rigidbody;
    public float velocityScale;

    private void Awake()
    {
        modelTransform.localRotation *= Quaternion.Euler(0, Random.Range(0, 360f), 0);
        Destroy(gameObject, 60f);
    }
}
