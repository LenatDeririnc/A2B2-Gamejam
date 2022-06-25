using UnityEngine;

namespace SystemInitializer.LevelSettings
{
    // [CreateAssetMenu(fileName = "LevelSettings", menuName = "LevelSettings", order = 0)]
    public abstract class LevelSettings : ScriptableObject
    {
        protected SystemsContainer _systems = new SystemsContainer();

        public SystemsContainer Systems => _systems;

        public virtual void CreateSystems()
        {
        }
    }
}