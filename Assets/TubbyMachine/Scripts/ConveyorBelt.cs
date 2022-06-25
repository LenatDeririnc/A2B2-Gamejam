using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public MeshRenderer conveyorRenderer;
    public Transform[] conveyorWheels;
    
    public float conveyorSpeed;
    public float conveyorWheelSpeed;

    public float scrollSpeed;
    public float maxScrollSpeed = 5f;
    public float velocitySpeed;

    [Header("Audio settings")]
    public AudioSource beltLoop;
    public float minAudioScrollSpeed = 0.1f;
    public float maxAudioScrollSpeed = 2f;

    public float minAudioPitch = 0.7f;
    public float maxAudioPitch = 1.1f;

    private float scrollPosition;
    private MaterialPropertyBlock _propertyBlock;
    private bool _isPoweredOn;

    private void Awake()
    {
        _propertyBlock = new MaterialPropertyBlock();
        beltLoop.Play();
        
        SetPoweredOn(false);
    }

    public void SetPoweredOn(bool isPoweredOn)
    {
        _isPoweredOn = isPoweredOn;

        if (!isPoweredOn)
            beltLoop.Stop();
        else
            beltLoop.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        var otherRigidbody = other.attachedRigidbody;

        var speed = _isPoweredOn ? scrollSpeed * velocitySpeed : 0f;
        var verticalVelocity = Vector3.Project(otherRigidbody.velocity, Vector3.up);
        
        otherRigidbody.velocity = verticalVelocity + transform.right * speed;
    }

    public void Update()
    {
        var speed = _isPoweredOn ? scrollSpeed : 0f;
        scrollPosition += speed * Time.deltaTime * maxScrollSpeed;
        
        _propertyBlock.SetVector("_BaseColorMap_ST", new Vector4(1f, 1f, scrollPosition * conveyorSpeed));
        conveyorRenderer.SetPropertyBlock(_propertyBlock);

        foreach (var wheel in conveyorWheels)
            wheel.localRotation = Quaternion.Euler(0, scrollPosition * conveyorWheelSpeed, 0);

        var volume = Mathf.InverseLerp(minAudioScrollSpeed, maxAudioScrollSpeed, Mathf.Abs(speed * maxScrollSpeed));
        beltLoop.volume = volume;
        beltLoop.pitch = Mathf.Lerp(minAudioPitch, maxAudioPitch, volume);
    }
}
