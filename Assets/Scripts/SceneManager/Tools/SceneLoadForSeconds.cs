using System.Collections;
using SceneManager.ScriptableObjects;
using SystemInitializer;
using UnityEngine;

namespace SceneManager.Tools
{
    public class SceneLoadForSeconds : MonoBehaviour
    {
        public SceneLink loadScene;
        public float waitSeconds = 5f;
        void Start()
        {
            StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(waitSeconds);
            ContextsContainer.GetContext<SceneLoaderContext>().LoadScene(loadScene);
        }
    }
}
