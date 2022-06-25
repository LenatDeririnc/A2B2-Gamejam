using System;
using Cinemachine;
using SystemInitializer;
using SystemInitializer.Systems.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace Movement
{
    public class MovementPoint : MonoBehaviour
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

        public Canvas inputCanvas;
        public CinemachineVirtualCamera VirtualCamera;

        [Space]
        public MovementPoint ForwardPoint;
        public MovementPoint BackPoint;
        public MovementPoint LeftPoint;
        public MovementPoint RightPoint;
        
        [Space]
        public Button ForwardButton;
        public Button LeftButton;
        public Button RightButton;
        public Button BackButton;

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
    }
}