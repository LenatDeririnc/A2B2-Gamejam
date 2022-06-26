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
            if (movementContext == null)
                return;

            CurrentMovementPoint = movementContext.StartMovementPoint;
            CurrentMovementPoint.SetCameraToThisPosition();
        }

        public void MoveForward()
        {
            if (CurrentMovementPoint != null) CurrentMovementPoint.ExecuteNextPoint(MoveDirectionAction.Forward);
        }

        public void MoveBackward()
        {
            if (CurrentMovementPoint != null) CurrentMovementPoint.ExecuteNextPoint(MoveDirectionAction.Backward);
        }

        public void MoveLeft()
        {
            if (CurrentMovementPoint != null) CurrentMovementPoint.ExecuteNextPoint(MoveDirectionAction.Left);
        }

        public void MoveRight()
        {
            if (CurrentMovementPoint != null) CurrentMovementPoint.ExecuteNextPoint(MoveDirectionAction.Right);
        }
    }
}