using SceneManager;
using SystemInitializer.Interfaces;

namespace SystemInitializer.Systems.SceneLoading
{
    public class SceneLoadingSystem : IAwakeSystem
    {
        public void Awake()
        {
            var curtain = ContextsContainer.GetContext<LoadingCurtainContext>();
            curtain.Init();

            ContextsContainer.GetContext<SceneLoaderContext>().OnStartLoadScene +=
                SystemsBootstrapper.Instance.OnChangeScene;
        }
    }
}