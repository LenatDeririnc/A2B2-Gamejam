using Cinemachine;
using Movement;
using SystemInitializer.Interfaces;
using SystemInitializer.Systems.Cinemachine;
using SystemInitializer.Systems.Input;
using UnityEngine.InputSystem;

namespace SystemInitializer.Systems.Movement
{
    public class MovementSystem : IAwakeSystem, ITerminateSystem
    {
        private MovementContext MovementContext => ContextsContainer.GetContext<MovementContext>();
        private MovementPoint currentMovementPoint
        {
            get => MovementContext.CurrentMovementPoint;
            set => MovementContext.CurrentMovementPoint = value;
        }

        private CinemachineBrain brain => ContextsContainer.GetContext<BrainContext>().Brain;
        
        public void Awake()
        {
            var movementContext = ContextsContainer.GetContext<MovementContext>();
            movementContext.ResetStartingPoint += ResetStartingPoint;
            
            var inputContext = ContextsContainer.GetContext<InputContext>();
            inputContext.actions.CameraMovement.MoveForward.performed += MoveForward;
            inputContext.actions.CameraMovement.MoveBack.performed += MoveBackward;
            inputContext.actions.CameraMovement.MoveLeft.performed += MoveLeft;
            inputContext.actions.CameraMovement.MoveRight.performed += MoveRight;
        }

        public void Terminate()
        {
            var inputContext = ContextsContainer.GetContext<InputContext>();
            inputContext.actions.CameraMovement.Disable();
        }

        private void ResetStartingPoint()
        {
            var movementContext = ContextsContainer.GetContext<MovementContext>();
            var inputContext = ContextsContainer.GetContext<InputContext>();
            
            if (movementContext == null)
                return;

            currentMovementPoint = movementContext.StartMovementPoint;

            inputContext.actions.CameraMovement.Enable();
            
            SetCameraToPosition(currentMovementPoint);
        }

        private void SetCameraToPosition(MovementPoint point)
        {
            brain.enabled = false;
            UnsetButtons(currentMovementPoint);
            currentMovementPoint.Disable();
            currentMovementPoint = point;
            currentMovementPoint.Enable(() => SetButtons(currentMovementPoint));
            currentMovementPoint.OnEnterCamera();
            SetButtons(currentMovementPoint);
            brain.enabled = true;
        }

        private void MoveCameraToPosition(MovementPoint point)
        {
            UnsetButtons(currentMovementPoint);
            currentMovementPoint.Disable();
            currentMovementPoint = point;
            currentMovementPoint.Enable(() => SetButtons(currentMovementPoint));
        }

        private void UnsetButtons(MovementPoint point)
        {
            if (point == null)
                return;
            
            if (point.ForwardButton != null) point.ForwardButton.onClick.RemoveAllListeners();
            if (point.BackButton != null) point.BackButton.onClick.RemoveAllListeners();
            if (point.LeftButton != null) point.LeftButton.onClick.RemoveAllListeners();
            if (point.RightButton != null) point.RightButton.onClick.RemoveAllListeners();
        }

        private void SetButtons(MovementPoint point)
        {
            if (point == null)
                return;
            
            if (point.ForwardButton != null) point.ForwardButton.onClick.AddListener(MoveForward);
            if (point.BackButton != null) point.BackButton.onClick.AddListener(MoveBackward);
            if (point.LeftButton != null) point.LeftButton.onClick.AddListener(MoveLeft);
            if (point.RightButton != null) point.RightButton.onClick.AddListener(MoveRight);
        }

        private void MoveForward(InputAction.CallbackContext callbackContext) => MoveForward();

        private void MoveForward()
        {
            if (currentMovementPoint == null)
                return;
            
            if (currentMovementPoint.ForwardPoint == null)
                return;
            
            MoveCameraToPosition(currentMovementPoint.ForwardPoint);
        }

        private void MoveBackward(InputAction.CallbackContext callbackContext) => MoveBackward();

        private void MoveBackward()
        {
            if (currentMovementPoint == null)
                return;
            
            if (currentMovementPoint.BackPoint == null)
                return;
            
            MoveCameraToPosition(currentMovementPoint.BackPoint);
        }


        private void MoveLeft(InputAction.CallbackContext callbackContext) => MoveLeft();

        private void MoveLeft()
        {
            if (currentMovementPoint == null)
                return;
            
            if (currentMovementPoint.LeftPoint == null)
                return;
            
            MoveCameraToPosition(currentMovementPoint.LeftPoint);
        }

        private void MoveRight(InputAction.CallbackContext callbackContext) => MoveRight();

        private void MoveRight()
        {
            if (currentMovementPoint == null)
                return;
            
            if (currentMovementPoint.RightPoint == null)
                return;
            
            MoveCameraToPosition(currentMovementPoint.RightPoint);
        }
    }
}