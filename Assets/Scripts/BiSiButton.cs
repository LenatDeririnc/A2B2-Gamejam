using System;
using UnityEngine;

public class BiSiButton : MonoBehaviour
{
    public Action Action;
    
    private void OnMouseDown()
    {
        Action?.Invoke();
    }
}