using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common.MainMenu.Buttons
{
    public class ExitGameButton : MonoBehaviour
    {
        [SerializeField] private Button Button;

        private void Start()
        {
            Button.onClick.AddListener(Exit);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}