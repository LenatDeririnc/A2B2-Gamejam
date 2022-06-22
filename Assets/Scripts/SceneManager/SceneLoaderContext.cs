using System;
using System.Collections;
using System.Collections.Generic;
using SceneManager.ScriptableObjects;
using SystemInitializer;
using SystemInitializer.Systems.SceneLoading;
using UnityEngine;

namespace SceneManager
{
    public class SceneLoaderContext : MonoBehaviourContext
    {
        [SerializeField] private List<SceneLink> scenes;
        private LoadingCurtainContext LoadingCurtainContext => ContextsContainer.GetContext<LoadingCurtainContext>();

        public void LoadScene(SceneLink sceneLink)
        {
            LoadingCurtainContext.Show(() => StartCoroutine(LoadSceneCoroutine(sceneLink, LoadingCurtainContext.Hide)));
        }

        private IEnumerator LoadSceneCoroutine(SceneLink sceneLink, Action onLoaded = null)
        {
            AsyncOperation waitSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneLink.sceneName);

            while (!waitSceneAsync.isDone)
                yield return null;
            
            onLoaded?.Invoke();
        }
    }
}