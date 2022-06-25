using System;
using Cinemachine;
using SystemInitializer;
using SystemInitializer.Systems.Cinemachine;
using SystemInitializer.Systems.Movement;
using UnityEngine;

namespace Movement
{
    public class MovementPoint : ButtonAction
    {
        private Transform _transform;

        public Transform Transform
        {
            get
            {
                if (_transform == null)
                    _transform = transform;
                return _transform;
            }
        }

        public bool isStartPosition;

        private Action onEnterActions;

        public Canvas inputCanvas;
        public CinemachineVirtualCamera VirtualCamera;

        [Space]
        public ButtonAction ForwardPoint;
        public ButtonAction BackPoint;
        public ButtonAction LeftPoint;
        public ButtonAction RightPoint;
        
        [Space]
        public UnityEngine.UI.Button ForwardButton;
        public UnityEngine.UI.Button LeftButton;
        public UnityEngine.UI.Button RightButton;
        public UnityEngine.UI.Button BackButton;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            var t = transform;
            Vector3 tpos = t.position;
            Vector3 tfofward = t.forward;
            Vector3 tright = t.right;
            Vector3 tup = t.up;
            Gizmos.DrawLine(tpos, tpos + tfofward * 2);
            Gizmos.DrawLine(tpos + tfofward * 2, tpos + tfofward * 1 + tright * 1);
            Gizmos.DrawLine(tpos + tfofward * 2, tpos + tfofward * 1 - tright * 1);
        }

        public void OnEnterCamera()
        {
            inputCanvas.gameObject.SetActive(true);
            inputCanvas.enabled = true;
            onEnterActions?.Invoke();
        }

        public void OnExitCamera()
        {
            inputCanvas.gameObject.SetActive(false);
        }

        public void Enable(Action onEnterActions = null)
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                VirtualCamera.Priority = 100;
                OnEnterCamera();
                return;
            }

            this.onEnterActions = onEnterActions;

            ContextsContainer.GetContext<BrainContext>().OnReachVirtualCamera += OnEnterCamera;

            VirtualCamera.Priority = 100;
        }

        public void Disable()
        {
            VirtualCamera.Priority = 0;
            
            if (Application.isEditor && !Application.isPlaying)
            {
                OnExitCamera();
                return;
            }
            
            ContextsContainer.GetContext<BrainContext>().OnReachVirtualCamera -= OnEnterCamera;
            OnExitCamera();
        }

        public override void Execute()
        {
            var movementContext = ContextsContainer.GetContext<MovementContext>();
            movementContext.CurrentMovementPoint.Disable();
            movementContext.CurrentMovementPoint = this;
            Enable(SetButtons);
            MoveCameraToPosition();
        }
        
        public void UnsetButtons()
        {
            if (ForwardButton != null) ForwardButton.onClick.RemoveAllListeners();
            if (BackButton != null) BackButton.onClick.RemoveAllListeners();
            if (LeftButton != null) LeftButton.onClick.RemoveAllListeners();
            if (RightButton != null) RightButton.onClick.RemoveAllListeners();
        }
        
        public void SetButtons()
        {
            if (ForwardButton != null && ForwardPoint != null) ForwardButton.onClick.AddListener(ForwardPoint.Execute);
            if (BackButton != null && BackPoint != null) BackButton.onClick.AddListener(BackPoint.Execute);
            if (LeftButton != null && LeftPoint != null) LeftButton.onClick.AddListener(LeftPoint.Execute);
            if (RightButton != null && RightPoint != null) RightButton.onClick.AddListener(RightPoint.Execute);
        }
        
        public void SetCameraToThisPosition()
        {
            var brain = ContextsContainer.GetContext<BrainContext>().Brain;

            brain.enabled = false;
            UnsetButtons();
            Disable();
            ContextsContainer.GetContext<MovementContext>().CurrentMovementPoint = this;
            Enable();
            OnEnterCamera();
            SetButtons();
            brain.enabled = true;
        }
        
        public void MoveCameraToPosition()
        {
            UnsetButtons();
            Disable();
            ContextsContainer.GetContext<MovementContext>().CurrentMovementPoint = this;
            Enable(SetButtons);
        }

    }
}