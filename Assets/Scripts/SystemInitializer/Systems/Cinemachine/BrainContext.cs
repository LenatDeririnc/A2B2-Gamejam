﻿using System;
using Cinemachine;

namespace SystemInitializer.Systems.Cinemachine
{
    public class BrainContext : MonoBehaviourContext
    {
        public CinemachineBrain Brain;
        public Action OnReachVirtualCamera;
        public Action OnStartReachingVirtualCamera;
    }
}