using System;
using SceneManager;
using SceneManager.ScriptableObjects;
using UnityEngine;

namespace SystemInitializer.Systems
{
    public class DayContext : MonoBehaviourContext
    {
        public SceneLink NextScene;

        public void EndDay()
        {
            ContextsContainer.GetContext<SceneLoaderContext>().LoadScene(NextScene);
        }

#if UNITY_EDITOR
        
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.P))
            {
                EndDay();
            }
        }
        
#endif
    }
}