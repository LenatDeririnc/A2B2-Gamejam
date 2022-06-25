using Movement;
using SystemInitializer.Systems.Cinemachine;
using SystemInitializer.Systems.Input;
using UnityEngine;

namespace SystemInitializer.Systems.Movement
{
    public class MovementContext : MonoBehaviourContext
    {
        public MovementPoint StartMovementPoint;
        [HideInInspector] public MovementPoint CurrentMovementPoint;

        public void ResetStartingPoint()
        {
            var movementContext = ContextsContainer.GetContext<MovementContext>();
            var inputContext = ContextsContainer.GetContext<InputContext>();
            
            if (movementContext == null)
                return;

            CurrentMovementPoint = movementContext.StartMovementPoint;
            
            CurrentMovementPoint.Enable();
            CurrentMovementPoint.OnEnterCamera();

            inputContext.actions.CameraMovement.Enable();
            
            CurrentMovementPoint.SetCameraToThisPosition();
        }

        public void MoveForward()
        {
            if (CurrentMovementPoint != null) CurrentMovementPoint.ForwardPoint?.Execute();
        }

        public void MoveBackward()
        {
            if (CurrentMovementPoint != null) CurrentMovementPoint.BackPoint?.Execute();
        }

        public void MoveLeft()
        {
            if (CurrentMovementPoint != null) CurrentMovementPoint.LeftPoint?.Execute();
        }

        public void MoveRight()
        {
            if (CurrentMovementPoint != null) CurrentMovementPoint.RightPoint?.Execute();
        }
    }
}