using Cinemachine;
using Movement;
using SystemInitializer.Interfaces;
using SystemInitializer.Systems.Input;
using UnityEngine;
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

        private CinemachineBrain brain;
        
        public void Awake()
        {
            var movementContext = ContextsContainer.GetContext<MovementContext>();
            var inputContext = ContextsContainer.GetContext<InputContext>();
            
            if (movementContext == null || !movementContext.enabled)
                return;

            currentMovementPoint = movementContext.StartMovementPoint;
            brain = movementContext.Brain;

            inputContext.actions.CameraMovement.MoveForward.performed += MoveForward;
            inputContext.actions.CameraMovement.MoveBack.performed += MoveBackward;
            inputContext.actions.CameraMovement.MoveLeft.performed += MoveLeft;
            inputContext.actions.CameraMovement.MoveRight.performed += MoveRight;
            inputContext.actions.CameraMovement.Enable();
            
            SetCameraToPosition(currentMovementPoint);
        }
        
        public void Terminate()
        {
            var inputContext = ContextsContainer.GetContext<InputContext>();
            
            inputContext.actions.CameraMovement.MoveForward.performed -= MoveForward;
            inputContext.actions.CameraMovement.MoveBack.performed -= MoveBackward;
            inputContext.actions.CameraMovement.MoveLeft.performed -= MoveLeft;
            inputContext.actions.CameraMovement.MoveRight.performed -= MoveRight;
            inputContext.actions.CameraMovement.Disable();
        }

        public void SetCameraToPosition(MovementPoint point)
        {
            brain.enabled = false;
            MoveCameraToPosition(point);
            brain.enabled = true;
        }

        public void MoveCameraToPosition(MovementPoint point)
        {
            currentMovementPoint.Disable();
            point.Enable();
        }

        public void MoveForward(InputAction.CallbackContext callbackContext)
        {
            if (currentMovementPoint == null)
                return;
            
            if (currentMovementPoint.ForwardPoint == null)
                return;
            
            MoveCameraToPosition(currentMovementPoint.ForwardPoint);

            currentMovementPoint = currentMovementPoint.ForwardPoint;
        }

        public void MoveBackward(InputAction.CallbackContext callbackContext)
        {
            if (currentMovementPoint == null)
                return;
            
            if (currentMovementPoint.BackPoint == null)
                return;
            
            MoveCameraToPosition(currentMovementPoint.BackPoint);

            currentMovementPoint = currentMovementPoint.BackPoint;
        }

        public void MoveLeft(InputAction.CallbackContext callbackContext)
        {
            if (currentMovementPoint == null)
                return;
            
            if (currentMovementPoint.LeftPoint == null)
                return;
            
            MoveCameraToPosition(currentMovementPoint.LeftPoint);

            currentMovementPoint = currentMovementPoint.LeftPoint;
        }

        public void MoveRight(InputAction.CallbackContext callbackContext)
        {
            if (currentMovementPoint == null)
                return;
            
            if (currentMovementPoint.RightPoint == null)
                return;
            
            MoveCameraToPosition(currentMovementPoint.RightPoint);

            currentMovementPoint = currentMovementPoint.RightPoint;
        }
    }
}