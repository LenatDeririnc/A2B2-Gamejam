using System;
using System.Collections;
using SystemInitializer;
using UnityEngine;

namespace SceneManager
{
    public class LoadingCurtainContext : MonoBehaviourContext
    {
        public CanvasGroup canvasGroup;
        public float fadeSpeed = 1;
        public bool HideCurtainOnStart;

        public void Init()
        {
            if (HideCurtainOnStart)
            {
                SetTransparency(1);
                Hide();
            }
            else
            {
                SetTransparency(0);
            }
        }

        public void Show(Action loadSceneAction) => 
            StartCoroutine(FadeOut(loadSceneAction));

        public void Hide() =>
            StartCoroutine(FadeIn());

        public void SetTransparency(float value) => 
            canvasGroup.alpha = value;

        private IEnumerator FadeOut(Action loadSceneAction)
        {
            canvasGroup.gameObject.SetActive(true);
            
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += fadeSpeed * Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = 1;

            loadSceneAction();
        }

        private IEnumerator FadeIn()
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
                yield return null;
            }
            
            canvasGroup.alpha = 0;
            
            canvasGroup.gameObject.SetActive(false);
        }
    }
}