﻿using System;
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
        
        private Action onEnterActions;
        private Action onExitActions;

        public Canvas inputCanvas;
        public CinemachineVirtualCamera VirtualCamera;

        [Space]
        public ButtonAction ForwardPoint;
        public ButtonAction BackPoint;
        public ButtonAction LeftPoint;
        public ButtonAction RightPoint;
        
        [Space]
        public UnityEngine.UI.Button ForwardButton;
        public UnityEngine.UI.Button BackButton;
        public UnityEngine.UI.Button LeftButton;
        public UnityEngine.UI.Button RightButton;

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
            
            ForwardButton.gameObject.SetActive(ForwardPoint != null);
            BackButton.gameObject.SetActive(BackPoint != null);
            LeftButton.gameObject.SetActive(LeftPoint != null);
            RightButton.gameObject.SetActive(RightPoint != null);
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

            if (onEnterActions != null)
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
            
            onExitActions?.Invoke();
        }

        public override void Execute()
        {
            MoveCameraToPosition();
        }

        public void Execute(Action onEnterAction, Action onExitAction)
        {
            onEnterActions = onEnterAction;
            onExitActions = onExitAction;
            MoveCameraToPosition(onEnterAction);
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
            ContextsContainer.GetContext<MovementContext>().CurrentMovementPoint.UnsetButtons();
            ContextsContainer.GetContext<MovementContext>().CurrentMovementPoint.Disable();
            ContextsContainer.GetContext<MovementContext>().CurrentMovementPoint = this;
            Enable();
            OnEnterCamera();
            SetButtons();
            brain.enabled = true;
        }
        
        public void MoveCameraToPosition()
        {
            ContextsContainer.GetContext<MovementContext>().CurrentMovementPoint.UnsetButtons();
            ContextsContainer.GetContext<MovementContext>().CurrentMovementPoint.Disable();
            ContextsContainer.GetContext<MovementContext>().CurrentMovementPoint = this;
            Enable(SetButtons);
        }
        
        public void MoveCameraToPosition(Action onReachAction)
        {
            ContextsContainer.GetContext<MovementContext>().CurrentMovementPoint.UnsetButtons();
            ContextsContainer.GetContext<MovementContext>().CurrentMovementPoint.Disable();
            ContextsContainer.GetContext<MovementContext>().CurrentMovementPoint = this;
            Enable(onReachAction);
        }

    }
}