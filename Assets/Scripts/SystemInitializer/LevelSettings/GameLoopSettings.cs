using SystemInitializer.Systems.Cinemachine;
using SystemInitializer.Systems.Movement;
using UnityEngine;

namespace SystemInitializer.LevelSettings
{
    [CreateAssetMenu(menuName = "Create GameLoopSettings", fileName = "GameLoopSettings", order = 0)]
    public class GameLoopSettings : LevelSettings
    {
        public override void CreateSystems()
        {
            _systems.Add(new BrainSystem());
            _systems.Add(new MovementSystem());
        }
    }
}