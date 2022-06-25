using SystemInitializer.Interfaces;
using UnityEngine;

namespace SystemInitializer.Systems.Cinemachine
{
    public class BrainSystem : IAwakeSystem, IUpdateSystem
    {
        private BrainContext _context;
        private bool isStartReached;
        private bool isReached;
        
        public void Awake()
        {
            _context = ContextsContainer.GetContext<BrainContext>();
            // _context.OnStartReachingVirtualCamera += () => Debug.Log("reaching");
            // _context.OnReachVirtualCamera += () => Debug.Log("reached");
        }


        public void Update()
        {
            if (_context.Brain.IsBlending && !isStartReached)
            {
                isStartReached = true;
                isReached = false;
                _context.OnStartReachingVirtualCamera?.Invoke();
            }

            if (!_context.Brain.IsBlending && !isReached)
            {
                isStartReached = false;
                isReached = true;
                _context.OnReachVirtualCamera?.Invoke();
            }
        }
    }
}