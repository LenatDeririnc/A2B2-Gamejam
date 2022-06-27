using System;
using Cinemachine;
using UnityEngine.Events;

namespace SystemInitializer.Systems.Cinemachine
{
    public class BrainContext : MonoBehaviourContext
    {
        public CinemachineBrain Brain;
        public UnityEvent OnReachVirtualCamera;
        public UnityEvent OnStartReachingVirtualCamera;
        
        public bool isStartReached;
        public bool isReached;

        public bool checkNextFrame;
    }
}