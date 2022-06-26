using System;
using UnityEngine;

namespace CrystalCrusher.Scripts
{
    public class CrystalTortureMiniGame : MonoBehaviour
    {
        public CrystalTortureRoom tortureRoom;
        public bool startTestGame;

        private void Awake()
        {
            if(startTestGame)
                StartGame(() =>
                {
                    Debug.Log("Completed");
                });
        }

        public void StartGame(Action onComplete)
        {
            tortureRoom.onGameCompleted = onComplete;
        }
    }
}