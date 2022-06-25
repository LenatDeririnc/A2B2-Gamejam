using System;
using System.Collections.Generic;
using ThreeDISevenZeroR.Utils;
using UnityEngine;

public class TubbyCustardPanel : MonoBehaviour
{
    [Header("On Off Switch")]
    public Collider onOffButton;
    public Transform onOffTransform;
    public float offPosition = -30f;
    public float onPosition;
    public AudioSource clickSource;
    public AmogusDispenser dispenserObject;
    
    [Header("Belt joystick")]
    public Collider beltRightButton;
    public Transform beltTransform;
    public float beltTiltAmount;
    public ConveyorBelt beltObject;
    
    [Header("Tap Joystick")]
    public Collider tapOnOffButton;
    public Transform tapTransform;
    public float tapTiltAmount;
    public CustardTap tapObject;

    [Header("Splort")]
    public Collider splortButton;
    public SkinnedMeshRenderer splortButtonRenderer;
    
    [Header("Animation")]
    public Spring animationSpring;
    public float hoverPressAmount;

    private SpringFloatTween _onOffTween;
    private SpringFloatTween _beltTween;
    private SpringFloatTween _tapTween;
    private SpringFloatTween _splortTween;

    private Quaternion _onOffInitialRotation;
    private Quaternion _tapInitialRotation;
    private Quaternion _beltInitialRotation;

    private readonly List<Collider> _registeredColliders = new();

    public void SetInteractable(bool isInteractable)
    {
        foreach (var c in _registeredColliders)
            c.gameObject.SetActive(isInteractable);
    }

    private void Awake()
    {
        _onOffInitialRotation = onOffTransform.localRotation;
        _beltInitialRotation = beltTransform.localRotation;
        _tapInitialRotation = tapTransform.localRotation;
        
        _onOffTween = new SpringFloatTween { spring = animationSpring };
        _beltTween = new SpringFloatTween { spring = animationSpring };
        _tapTween = new SpringFloatTween { spring = animationSpring };
        _splortTween = new SpringFloatTween { spring = animationSpring };

        AddButton(onOffButton, _onOffTween, offPosition, onPosition, true, isOn =>
        {
            clickSource.Play();
            dispenserObject.OpenDoors();
            
            beltObject.SetPoweredOn(isOn);
            tapObject.SetPoweredOn(isOn);
        });
        
        AddJoystick(beltRightButton, _beltTween, 0.1f, beltTiltAmount);
        
        AddButton(tapOnOffButton, _tapTween, 0f, tapTiltAmount, true, isOn =>
        {
            tapObject.SetOpened(isOn);
        });

        AddButton(splortButton, _splortTween, 0f, 100f, false, isPressed =>
        {
            tapObject.Splort();
        });
        
        SetInteractable(false);
    }

    private void Update()
    {
        _onOffTween.Update(Time.deltaTime);
        _beltTween.Update(Time.deltaTime);
        _tapTween.Update(Time.deltaTime);
        _splortTween.Update(Time.deltaTime);

        onOffTransform.localRotation = _onOffInitialRotation * Quaternion.Euler(0, 0, _onOffTween.current);
        beltTransform.localRotation = _beltInitialRotation * Quaternion.Euler(0, 0, _beltTween.current);
        tapTransform.localRotation = _tapInitialRotation * Quaternion.Euler(_tapTween.current, 0, 0);
        
        splortButtonRenderer.SetBlendShapeWeight(0, _splortTween.current);
        beltObject.scrollSpeed = -_beltTween.current / beltTiltAmount;
    }

    private void AddButton(Collider c, SpringFloatTween tween, float inactivePosition, 
        float pressPosition, bool isButton, Action<bool> onPressed)
    {
        var listener = c.gameObject.AddComponent<ClickListener>();
        listener.targetTween = tween;
        listener.inactivePosition = inactivePosition;
        listener.pressPosition = pressPosition;
        listener.inactiveHoverPosition = Mathf.Lerp(inactivePosition, pressPosition, hoverPressAmount);
        listener.pressHoverPosition = Mathf.Lerp(inactivePosition, pressPosition, 1f - hoverPressAmount);
        listener.isButton = isButton;
        listener.onPressed = onPressed;
        tween.target = inactivePosition;
        tween.Finish();
        _registeredColliders.Add(c);
    }

    private void AddJoystick(Collider c, SpringFloatTween tween, float dragScale, float maxAmplitude)
    {
        var listener = c.gameObject.AddComponent<JoystickListener>();
        listener.dragScale = dragScale;
        listener.maxAmplitude = maxAmplitude;
        listener.targetTween = tween;
        _registeredColliders.Add(c);
    }

    private class JoystickListener : MonoBehaviour
    {
        public float dragScale;
        public float maxAmplitude;
        public SpringFloatTween targetTween;
        public Vector3 lastMousePosition;

        private void OnMouseDown()
        {
            lastMousePosition = Input.mousePosition;
        }

        private void OnMouseDrag()
        {
            var newMousePosition = Input.mousePosition;
            var delta = newMousePosition - lastMousePosition;
            
            targetTween.target = Mathf.Clamp(targetTween.target + delta.x * dragScale, -maxAmplitude, maxAmplitude);

            lastMousePosition = newMousePosition;
        }

        private void OnMouseUp()
        {
            targetTween.target = 0f;
        }

        private void OnMouseUpAsButton()
        {
            targetTween.target = 0f;
        }

        private void OnDisable()
        {
            OnMouseUp();
        }
    }

    private class ClickListener : MonoBehaviour
    {
        public SpringFloatTween targetTween;
        public float inactivePosition;
        public float inactiveHoverPosition;
        public float pressHoverPosition;
        public float pressPosition;
        public bool isButton;
        public Action<bool> onPressed;
        
        private bool _isPressed;

        private void OnDisable()
        {
            OnMouseUp();
        }

        private void OnMouseEnter()
        {
            targetTween.target = _isPressed ? pressHoverPosition : inactiveHoverPosition; 
        }

        private void OnMouseExit()
        {
            targetTween.target = _isPressed ? pressPosition : inactivePosition;
        }

        private void OnMouseDown()
        {
            if (isButton)
            {
                _isPressed = !_isPressed;
                targetTween.target = _isPressed ? pressPosition : inactivePosition;
            }
            else
            {
                _isPressed = true;
                targetTween.target = pressPosition;
            }
            
            onPressed.Invoke(_isPressed);
        }

        private void OnMouseUp()
        {
            if (isButton)
                return;
            
            _isPressed = false;
            targetTween.target = inactivePosition;
        }
    }
}
