using System.Collections.Generic;
using SystemInitializer.Systems.Input;
using SystemInitializer.Systems.SceneLoading;
using UnityEngine;

namespace SystemInitializer
{
    public class SystemsBootstrapper : SimpleSingleton<SystemsBootstrapper>
    {
        [SerializeField] 
        private List<MonoBehaviourContext> _contextsList = new List<MonoBehaviourContext>();
        SystemsContainer _systems = new SystemsContainer();
    
        private void Awake()
        {
            InitSingleton(this, LoadSystems);
        }

        private void LoadSystems()
        {
            ContextsContainer.Initialize(_contextsList);

            // NOTE: добавлять сюда все инициализации систем
            _systems.Add(new InputSystem());
            _systems.Add(new SceneLoadingSystem());

            _systems.Awake();
        }

        private void Start()
        {
            _systems.Start();
        }

        private void Update()
        {
            _systems.Update();
        }

        private void LateUpdate()
        {
            _systems.LateUpdate();
        }
    }
}