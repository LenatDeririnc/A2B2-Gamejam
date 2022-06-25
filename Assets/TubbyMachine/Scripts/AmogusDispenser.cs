using System;
using ThreeDISevenZeroR.SensorKit;
using UnityEngine;

public class AmogusDispenser : MonoBehaviour
{
    public BoxOverlapSensor custardSensor = new();
    public Animator animator;

    public Transform vialDetectionPosition;
    public Action onVialFilled;

    private bool isFinished;

    private OverlapQuery VialQuery => new()
    {
        center = vialDetectionPosition.position,
        rotation = vialDetectionPosition.rotation,
        scale = vialDetectionPosition.localScale
    };

    public void OpenDoors()
    {
        if(!isFinished)
            animator.SetTrigger("IsOpened");
    }
    
    private void Update()
    {
        if (isFinished)
            return;
        
        if (custardSensor.Query(VialQuery))
        {
            var vial = custardSensor.HitCollider.GetComponentInParent<FillableVirusVial>();

            if (vial && vial.IsFilled)
            {
                onVialFilled?.Invoke();
                isFinished = true;
                animator.SetTrigger("IsClosed");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        custardSensor.DrawQueryPreviewGizmo(VialQuery);
    }
}
