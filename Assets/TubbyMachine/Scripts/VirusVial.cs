using UnityEngine;

public class VirusVial : MonoBehaviour
{
    public Transform progressScale;
    public float progress;

    private void Update()
    {
        progressScale.localScale = new Vector3(1f, 1f, progress);
        progressScale.gameObject.SetActive(progress > 0f);
    }
}
