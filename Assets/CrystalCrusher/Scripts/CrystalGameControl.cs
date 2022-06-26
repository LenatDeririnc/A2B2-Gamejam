using CrystalCrusher.Scripts;
using UnityEngine;

public class CrystalGameControl : MonoBehaviour
{
    public Material notPressMaterial;
    public Material pressMaterial;
    public Renderer render;

    public CrystalTortureRoom room;
    public bool isCannon;

    private void Awake()
    {
        OnMouseUp();
    }

    private void OnMouseDown()
    {
        var mats = render.materials;
        mats[2] = pressMaterial;
        render.materials = mats;

        if (isCannon)
        {
            room.BeginThrowCrystalSequence();
        }
        else
        {
            room.BeginHitSequence();
        }
    }
    
    private void OnMouseUp()
    {
        var mats = render.materials;
        mats[2] = notPressMaterial;
        render.materials = mats;
    }
}
