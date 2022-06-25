using System;
using SceneManager;
using SceneManager.ScriptableObjects;
using SystemInitializer;
using UnityEngine;

public class SceneLoadButton : MonoBehaviour
{
    [SerializeField] private SceneLink sceneLoad;
    private void OnMouseDown()
    {
        ContextsContainer.GetContext<SceneLoaderContext>().LoadScene(sceneLoad);
    }
}
