using SystemInitializer.Interfaces;
using UnityEngine;

namespace SystemInitializer.Systems.Cinemachine
{
    public class BrainSystem : IAwakeSystem, IUpdateSystem
    {
        private BrainContext _context;

        public void Awake()
        {
            _context = ContextsContainer.GetContext<BrainContext>();
            // _context.OnStartReachingVirtualCamera += () => Debug.Log("reaching");
            // _context.OnReachVirtualCamera += () => Debug.Log("reached");
        }

        public void Update()
        {
            if (_context.checkNextFrame)
            {
                _context.checkNextFrame = false;
                return;
            }
            
            if (_context.Brain.IsBlending && !_context.isStartReached)
            {
                _context.isStartReached = true;
                _context.isReached = false;
                _context.OnStartReachingVirtualCamera?.Invoke();
            }

            if (!_context.Brain.IsBlending && !_context.isReached)
            {
                _context.isStartReached = false;
                _context.isReached = true;
                _context.OnReachVirtualCamera?.Invoke();
            }
        }
    }
}