using System;
using UnityEngine;
using UnityEngine.Events;

namespace CrystalPachinko.Scripts
{
    public class PachinkoMiniGame : MonoBehaviour
    {
        public PachinkoPissShitMixer mixer;
        public UnityEvent OnEnd;

        public void StartGame(Action action)
        {
            mixer.onComplete = action;
        }

        public void StartGame()
        {
            StartGame(OnEnd.Invoke);
        }
    }
}