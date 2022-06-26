using System;
using UnityEngine;

namespace CrystalPachinko.Scripts
{
    public class PachinkoMiniGame : MonoBehaviour
    {
        public PachinkoPissShitMixer mixer;

        public void StartGame(Action action)
        {
            mixer.onComplete = action;
        }
    }
}