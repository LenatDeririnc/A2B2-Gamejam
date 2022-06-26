using System;
using UnityEngine;
using UnityEngine.Events;

namespace CrystalPachinko.Scripts
{
    public class PachinkoMiniGame : MonoBehaviour
    {
        public PachinkoPissShitMixer mixer;
        public GameObject handle;
        public UnityEvent OnEnd;

        public void StartGame(Action action)
        {
            handle.SetActive(true);
            mixer.onComplete = action;
        }

        public void StartGame()
        {
            StartGame(OnEnd.Invoke);
        }
    }
}