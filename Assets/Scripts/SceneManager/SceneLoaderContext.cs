using System;
using System.Collections;
using SceneManager.ScriptableObjects;
using SystemInitializer;
using UnityEngine;

namespace SceneManager
{
    public class SceneLoaderContext : MonoBehaviourContext
    {
        private LoadingCurtainContext LoadingCurtainContext => ContextsContainer.GetContext<LoadingCurtainContext>();

        public Action OnStartLoadScene;

        public void LoadScene(SceneLink sceneLink)
        {
            OnStartLoadScene?.Invoke();
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