using System;
using System.Collections;
using SystemInitializer;
using UnityEngine;

public class FadeContext : MonoBehaviourContext
{
    public float speed = 1;
    public CanvasGroup CanvasGroup;

    public void Show() => 
        StartCoroutine(FadeOut());

    public void Hide() =>
        StartCoroutine(FadeIn());
    
    public void Show(Action action = null) => 
        StartCoroutine(FadeOut(action));

    public void Hide(Action action = null) =>
        StartCoroutine(FadeIn(action));
    
    private IEnumerator FadeOut(Action action = null)
    {
        CanvasGroup.gameObject.SetActive(true);
            
        while (CanvasGroup.alpha < 1)
        {
            CanvasGroup.alpha += speed * Time.deltaTime;
            yield return null;
        }

        CanvasGroup.alpha = 1;

        action?.Invoke();
    }

    private IEnumerator FadeIn(Action action = null)
    {
        while (CanvasGroup.alpha > 0)
        {
            CanvasGroup.alpha -= speed * Time.deltaTime;
            yield return null;
        }
            
        CanvasGroup.alpha = 0;
        
        CanvasGroup.gameObject.SetActive(false);
        
        action?.Invoke();
    }
}