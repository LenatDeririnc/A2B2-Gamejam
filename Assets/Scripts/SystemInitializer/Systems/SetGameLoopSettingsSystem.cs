using SystemInitializer.Interfaces;
using SystemInitializer.Systems.Cinemachine;
using SystemInitializer.Systems.Movement;
using UnityEngine;

namespace SystemInitializer.Systems
{
    public class SetGameLoopSettingsSystem : IAwakeSystem
    {
        public void Awake()
        {
            var gameLoopSettingsContext = ContextsContainer.GetContext<GameLoopSettingsContext>();
            var movementContext = ContextsContainer.GetContext<MovementContext>();
            var brainContext = ContextsContainer.GetContext<BrainContext>();

            movementContext.StartMovementPoint = gameLoopSettingsContext.StartMovementPoint;
            movementContext.ResetStartingPoint();
            
            brainContext.Brain.ActiveBlend.Duration = 0.75f;
        }
    }
}