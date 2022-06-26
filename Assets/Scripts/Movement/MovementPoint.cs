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
        private BrainContext BrainContext => ContextsContainer.GetContext<BrainContext>();
        private MovementContext MovementContext => ContextsContainer.GetContext<MovementContext>();
        
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

        private void Awake()
        {
            SetButtons();
        }

        public void SetButtons()
        {
            ForwardButton.onClick.AddListener(() => ExecuteNextPoint(ForwardPoint));
            BackButton.onClick.AddListener(() => ExecuteNextPoint(BackPoint));
            LeftButton.onClick.AddListener(() => ExecuteNextPoint(LeftPoint));
            RightButton.onClick.AddListener(() => ExecuteNextPoint(RightPoint));
        }

        public override void Execute()
        {
            Enable();
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
            
            MovementContext.CurrentMovementPoint?.Disable();
            
            BrainContext.checkNextFrame = true;
            BrainContext.isReached = false;
            BrainContext.OnReachVirtualCamera += OnEnterCamera;

            VirtualCamera.Priority = 100;
            
            MovementContext.CurrentMovementPoint = this;
        }

        public void Disable()
        {
            VirtualCamera.Priority = 0;
            
            if (Application.isEditor && !Application.isPlaying)
            {
                OnExitCamera();
                return;
            }

            BrainContext.OnReachVirtualCamera -= OnEnterCamera;
            OnExitCamera();
            
            MovementContext.CurrentMovementPoint = null;
        }

        public void OnEnterCamera()
        {
            inputCanvas.gameObject.SetActive(true);
            inputCanvas.enabled = true;
            onEnterActions?.Invoke();
            
            ForwardButton.gameObject.SetActive(ForwardPoint != null && ForwardPoint.IsEnabled());
            BackButton.gameObject.SetActive(BackPoint != null && BackPoint.IsEnabled());
            LeftButton.gameObject.SetActive(LeftPoint != null && LeftPoint.IsEnabled());
            RightButton.gameObject.SetActive(RightPoint != null && RightPoint.IsEnabled());
        }

        public void OnExitCamera()
        {
            inputCanvas.gameObject.SetActive(false);
            onExitActions?.Invoke();
        }

        public void Execute(Action onEnterAction, Action onExitAction)
        {
            onEnterActions = onEnterAction;
            onExitActions = onExitAction;
            Enable(onEnterAction);
        }

        public void SetCameraToThisPosition()
        {
            BrainContext.Brain.enabled = false;
            MovementContext.CurrentMovementPoint?.Disable();
            Enable();
            BrainContext.Brain.enabled = true;
        }

        private void ExecuteNextPoint(ButtonAction button)
        {
            if (button == null)
                return;
            
            if (!button.IsEnabled())
                return;
            
            Disable();
            button.Execute();
        }

        public void ExecuteNextPoint(MoveDirectionAction action)
        {
            switch (action)
            {
                case MoveDirectionAction.Forward:
                    ExecuteNextPoint(ForwardPoint);
                    break;
                case MoveDirectionAction.Backward:
                    ExecuteNextPoint(BackPoint);
                    break;
                case MoveDirectionAction.Left:
                    ExecuteNextPoint(LeftPoint);
                    break;
                case MoveDirectionAction.Right:
                    ExecuteNextPoint(RightPoint);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }
    }

    public enum MoveDirectionAction
    {
        Forward,
        Backward,
        Left,
        Right
    }
}