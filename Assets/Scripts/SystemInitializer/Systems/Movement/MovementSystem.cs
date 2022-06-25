using SystemInitializer.Interfaces;
using SystemInitializer.Systems.Input;

namespace SystemInitializer.Systems.Movement
{
    public class MovementSystem : IAwakeSystem, ITerminateSystem
    {
        private MovementContext MovementContext => ContextsContainer.GetContext<MovementContext>();

        public void Awake()
        {
            MovementContext.ResetStartingPoint();

            var inputContext = ContextsContainer.GetContext<InputContext>();
            inputContext.actions.CameraMovement.MoveForward.performed += _ => MovementContext.MoveForward();
            inputContext.actions.CameraMovement.MoveBack.performed += _ => MovementContext.MoveBackward();
            inputContext.actions.CameraMovement.MoveLeft.performed += _ => MovementContext.MoveLeft();
            inputContext.actions.CameraMovement.MoveRight.performed += _ => MovementContext.MoveRight();
        }

        public void Terminate()
        {
            var inputContext = ContextsContainer.GetContext<InputContext>();
            inputContext.actions.CameraMovement.Disable();
        }

    }
}