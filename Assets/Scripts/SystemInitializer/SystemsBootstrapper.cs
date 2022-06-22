using System.Collections.Generic;
using AS.SystemInitializer.Interfaces;
using UnityEngine;

namespace AS.SystemInitializer
{
    public class SystemsBootstrapper : MonoBehaviour
    {
        [SerializeField] 
        private List<MonoBehaviourContext> _contextsList = new List<MonoBehaviourContext>();
        SystemsContainer _systems = new SystemsContainer();
    
        private void Awake()
        {
            ContextsContainer.Initialize(_contextsList);

            // NOTE: добавлять сюда все инициализации систем
            //_systems.Add(new ...System());

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