using SystemInitializer.Interfaces;

namespace SystemInitializer.Systems.Input
{
    public class InputSystem : IAwakeSystem
    {
        private InputContext _context;

        public void Awake()
        {
            _context = ContextsContainer.GetContext<InputContext>();
            _context.actions = new InputActions();
        }
    }
}