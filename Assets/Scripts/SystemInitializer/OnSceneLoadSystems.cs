using System.Collections.Generic;
using UnityEngine;

namespace SystemInitializer
{
    public class OnSceneLoadSystems : MonoBehaviour
    {
        [SerializeField] 
        private List<MonoBehaviourContext> _contextsList = new List<MonoBehaviourContext>();
        
        [SerializeField] private LevelSettings.LevelSettings _SelectedSettings;

        private void Awake()
        {
            ContextsContainer.OverrideContexts(_contextsList);
            _SelectedSettings.CreateSystems();
            
            _SelectedSettings.Systems.Awake();
        }
        
        private void Start()
        {
            _SelectedSettings.Systems.Start();
        }

        private void Update()
        {
            _SelectedSettings.Systems.Update();
        }

        private void LateUpdate()
        {
            _SelectedSettings.Systems.LateUpdate();
        }

        public void OnDestroy()
        {
            _SelectedSettings.Systems.Terminate();
        }
    }
}