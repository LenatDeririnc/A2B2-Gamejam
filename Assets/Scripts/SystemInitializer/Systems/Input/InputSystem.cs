using AS.SystemInitializer.Interfaces;

namespace AS.SystemInitializer.Systems.Input
{
    public class InputSystem : IAwakeSystem
    {
        private InputActions _actions;
        
        public void Awake()
        {
            _actions = new InputActions();
        }
    }
}