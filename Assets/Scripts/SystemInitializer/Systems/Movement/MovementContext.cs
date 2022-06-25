using Movement;
using UnityEngine;

namespace SystemInitializer.Systems.Movement
{
    public class MovementContext : MonoBehaviourContext
    {
        public MovementPoint StartMovementPoint;
        [HideInInspector] public MovementPoint CurrentMovementPoint;
    }
}