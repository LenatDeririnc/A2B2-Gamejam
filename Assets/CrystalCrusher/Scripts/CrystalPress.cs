using System;
using System.Collections;
using Cinemachine;
using ThreeDISevenZeroR.SensorKit;
using UnityEngine;

public class CrystalPress : MonoBehaviour
{
    public Animator pressAnimator;
    public AudioSource hitSource;
    public AudioSource chargeSource;
    public SphereOverlapSensor overlapSensor;
    public Transform sensorOrigin;
    public GameObject crushedCrystals;
    public CinemachineImpulseSource impulse;

    private bool _hasHit;

    public OverlapQuery Query => new OverlapQuery
    {
        center = sensorOrigin.position,
        rotation = sensorOrigin.rotation,
        scale = sensorOrigin.lossyScale
    };

    public void StartPress()
    {
        if (pressAnimator.GetCurrentAnimatorStateInfo(0).IsName("CrusherIdle"))
        {
            pressAnimator.SetTrigger("Activate");
            _hasHit = false;
            chargeSource.Play();
        }
    }
    
    public void OnPressActivate()
    {
        hitSource.Play();
        
        if (overlapSensor.Query(Query))
        {
            var crystal = overlapSensor.HitCollider.GetComponent<CrunchyZenCrystal>();

            if (crystal)
            {
                StartCoroutine(CallActionAfterDelay(() => { crystal.onHitOrMiss.Invoke(true); }, 0.5f));
                crystal.AnimateDestroy();
                _hasHit = true;
            }
        }
    }

    private IEnumerator CallActionAfterDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    public void OnPressSmashed()
    {
        impulse.GenerateImpulse();
        
        if(_hasHit) 
            crushedCrystals.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        overlapSensor.DrawQueryPreviewGizmo(Query);
    }
}
