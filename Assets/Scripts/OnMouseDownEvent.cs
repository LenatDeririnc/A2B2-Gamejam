using UnityEngine;
using UnityEngine.Events;

public class OnMouseDownEvent : MonoBehaviour
{
    public UnityEvent Event;

    public void OnMouseDown()
    {
        Event.Invoke();
    }
}