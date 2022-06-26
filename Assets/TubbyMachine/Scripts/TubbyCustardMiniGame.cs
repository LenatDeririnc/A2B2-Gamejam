using System;
using UnityEngine;
using UnityEngine.Events;

namespace TubbyMachine.Scripts
{
    public class TubbyCustardMiniGame : MonoBehaviour
    {
        public TubbyCustardPanel panel;
        public bool startTestGame;

        public UnityEvent OnFinishedEvents;
        
        public void StartGame(Action onFinished)
        {
            panel.SetInteractable(true);
            panel.dispenserObject.onVialFilled = () =>
            {
                panel.SetInteractable(false);
                onFinished?.Invoke();
            };
        }

        public void StartGame()
        {
            StartGame(() => OnFinishedEvents.Invoke());
        }

        private void Start()
        {
            if (startTestGame)
            {
                StartGame(() =>
                {
                    Debug.Log("Finished!");
                });
            }
        }
    }
}