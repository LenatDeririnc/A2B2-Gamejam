using System;
using System.Collections;
using System.Linq;
using ThreeDISevenZeroR.SensorKit;
using UnityEngine;

public class PachinkoPissShitMixer : MonoBehaviour
{
    public GameObject crystalPrefab;
    public Transform crystalSpawnPosition;
    public Animator animator;
    public AudioSource audioBlip;
    public AudioSource audioFinished;
    public MeshRenderer lcdRenderer;

    public Material redMaterial;
    public Material greenMaterial;
 
    public Material readyMaterial;
    public Action onComplete;
    
    [NonReorderable]
    public ContainerData[] containers;

    private bool _isFinished;

    public bool DropCrystal()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PachinkoReadyLeftRight") && 
            !animator.IsInTransition(0))
        {
            animator.SetTrigger("Drop");
            Instantiate(crystalPrefab, crystalSpawnPosition.position, Quaternion.identity);
            return true;
        }

        return false;
    }

    private void Update()
    {
        if(_isFinished)
            return;
        
        if (Input.GetButtonDown("Jump"))
        {
            DropCrystal();
        }

        foreach (var c in containers)
        {
            if (c.sensor.Query(GetCastQuery(c.position)))
            {
                if (!c.isLit)
                    audioBlip.Play();

                c.isLit = true;
                c.lightRenderer.sharedMaterial = greenMaterial;
            }
            else
            {
                c.isLit = false;
                c.lightRenderer.sharedMaterial = redMaterial;
            }
        }
        
        if (containers.All(c => c.isLit))
        {
            lcdRenderer.material = readyMaterial;
            _isFinished = true;
            audioFinished.Play();

            StartCoroutine(StartFinishSequence());
        }
    }

    private IEnumerator StartFinishSequence()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Finished");
        yield return new WaitForSeconds(1f);
        onComplete?.Invoke();
        onComplete = null;
    }

    private void OnDrawGizmos()
    {
        foreach (var containerData in containers)
            containerData.sensor.DrawQueryPreviewGizmo(GetCastQuery(containerData.position));
    }

    public OverlapQuery GetCastQuery(Transform t)
    {
        return new()
        {
            center = t.position,
            rotation = t.rotation,
            scale = t.lossyScale
        };
    }

    [Serializable]
    public class ContainerData
    {
        public Transform position;
        public BoxOverlapSensor sensor;
        public MeshRenderer lightRenderer;
        public bool isLit;
    }
}
