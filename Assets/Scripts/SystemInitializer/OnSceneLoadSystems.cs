using System.Collections.Generic;
using SystemInitializer.Systems.Cinemachine;
using SystemInitializer.Systems.Movement;
using UnityEngine;

namespace SystemInitializer
{
    public class OnSceneLoadSystems : MonoBehaviour
    {
        protected SystemsContainer _systems;

        [SerializeField] 
        private List<MonoBehaviourContext> _contextsList;

        private void Awake()
        {
            ContextsContainer.OverrideContexts(_contextsList);
            _systems = new SystemsContainer();
            _systems.Add(new BrainSystem());
            _systems.Add(new MovementSystem());

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

        public void OnDestroy()
        {
            _systems.Terminate();
        }
    }
}