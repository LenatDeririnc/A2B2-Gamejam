using System.Collections.Generic;
using SystemInitializer.Interfaces;

namespace SystemInitializer
{
    public class SystemsContainer : IAwakeSystem, IUpdateSystem, ILateUpdateSystem
    {
        List<IAwakeSystem> _initializeSystems = new List<IAwakeSystem>();
        private List<IStartSystem> _startSystems = new List<IStartSystem>();
        List<IUpdateSystem> _updateSystems = new List<IUpdateSystem>();
        List<ILateUpdateSystem> _lateUpdateSystems = new List<ILateUpdateSystem>();

        public void Add(ISystem system)
        {
            if (system is IAwakeSystem initializeSystem)
                _initializeSystems.Add(initializeSystem);
            if (system is IStartSystem startSystem)
                _startSystems.Add(startSystem);
            if (system is IUpdateSystem updateSystem)
                _updateSystems.Add(updateSystem);
            if (system is ILateUpdateSystem lateUpdateSystem) 
                _lateUpdateSystems.Add(lateUpdateSystem);
        }

        public void Awake()
        {
            foreach (var system in _initializeSystems)
            {
                system.Awake();
            }
        }

        public void Start()
        {
            foreach (var system in _startSystems)
            {
                system.Start();
            }
        }

        public void Update()
        {
            foreach (var system in _updateSystems)
            {
                system.Update();
            }
        }

        public void LateUpdate()
        {
            foreach (var system in _lateUpdateSystems)
            {
                system.LateUpdate();
            }
        }
    }
}