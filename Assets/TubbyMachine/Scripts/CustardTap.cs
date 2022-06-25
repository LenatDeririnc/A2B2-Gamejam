using ThreeDISevenZeroR.SensorKit;
using UnityEngine;

public class CustardTap : MonoBehaviour
{
    public Transform custardTap;
    public float openPosition;
    public float closePosition;
    public float moveSpeed;
    public Transform custardTip;
    public AudioSource custardLoopSource;
    public AudioSource custardSplortSource;
    public AudioClip[] splortSounds;
    public CustardBall custardBallPrefab;
    public float minThrowForce;
    public float maxThrowForce;
    public ParticleSystem splortParticles;

    public SphereCastSensor bottleCastSensor = new();

    private bool _isReady;
    private bool _isPouring;
    private float _currentPosition;
    private float _targetPosition;
    private bool _isPoweredOn;

    private CastQuery BottleQuery => new()
    {
        distance = 10f,
        ray = new Ray(custardTip.transform.position, Vector3.down),
        rotation = Quaternion.identity,
        scale = Vector3.one
    };
    
    public void SetPoweredOn(bool isPoweredOn)
    {
        _isPoweredOn = isPoweredOn;
    }

    public void Splort()
    {
        if(!_isPoweredOn)
            return;
        
        custardSplortSource.PlayOneShot(splortSounds[Random.Range(0, splortSounds.Length)]);

        var instance = Instantiate(custardBallPrefab, custardTip.position, Quaternion.identity);
        instance.rigidbody.AddForce(custardTip.transform.forward * Random.Range(minThrowForce, maxThrowForce));
        instance.rigidbody.AddForce(custardTap.transform.right * Random.Range(-minThrowForce, minThrowForce));
        
        splortParticles.Emit(100);
    }

    public void SetOpened(bool isOpened)
    {
        _targetPosition = isOpened ? openPosition : closePosition;
    }

    private void Awake()
    {
        _currentPosition = closePosition;
        _targetPosition = closePosition;
        
        SetPoweredOn(false);
    }

    private void Update()
    {
        if (_isPoweredOn)
        {
            _currentPosition = Mathf.MoveTowards(_currentPosition, _targetPosition, 
                moveSpeed * Time.deltaTime);
            
            if (_currentPosition != _targetPosition)
            {
                if(!custardLoopSource.isPlaying)
                    custardLoopSource.Play();
            }
            else
            {
                if(custardLoopSource.isPlaying)
                    custardLoopSource.Stop();
            }
        }
        else
        {
            custardLoopSource.Stop();
        }
        
        custardTap.localRotation = Quaternion.Euler(0, _currentPosition, 0);
    }

    private void OnDrawGizmos()
    {
        bottleCastSensor.DrawQueryPreviewGizmo(BottleQuery);
    }
}
