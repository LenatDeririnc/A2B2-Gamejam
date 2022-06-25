using Cinemachine;
using Movement;
using UnityEngine;

namespace SystemInitializer.Systems.Movement
{
    public class MovementContext : MonoBehaviourContext
    {
        public CinemachineBrain Brain;
        public MovementPoint StartMovementPoint;
        [HideInInspector] public MovementPoint CurrentMovementPoint;
    }
}