using SceneManager;
using SceneManager.ScriptableObjects;

namespace SystemInitializer.Systems
{
    public class DayContext : MonoBehaviourContext
    {
        public SceneLink NextScene;

        public void EndDay()
        {
            ContextsContainer.GetContext<SceneLoaderContext>().LoadScene(NextScene);
        }
    }
}