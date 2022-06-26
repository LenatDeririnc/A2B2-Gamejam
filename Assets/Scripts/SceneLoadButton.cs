using SceneManager;
using SceneManager.ScriptableObjects;
using SystemInitializer;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadButton : MonoBehaviour
{
    [SerializeField] private Button Button;
    [SerializeField] private SceneLink sceneLoad;

    private void Start()
    {
        Button.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        ContextsContainer.GetContext<SceneLoaderContext>().LoadScene(sceneLoad);
    }
}
