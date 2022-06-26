using System.Collections;
using SceneManager;
using SceneManager.ScriptableObjects;
using SystemInitializer;
using SystemInitializer.Systems;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour
{
    [SerializeField] private SceneLink NextScene;
    
    [SerializeField] private Button Button;
    [SerializeField] private Canvas MainMenuCanvas;
    [SerializeField] private Canvas SignCanvas;
    [SerializeField] private CanvasGroup MainMenuCanvasGroup;
    [SerializeField] private CanvasGroup SignCanvasGroup;

    [SerializeField] private float MainMenuFadeSpeed = 1;
    [SerializeField] private float SignFadeSpeed = 1;
    [SerializeField] private float waitForSeconds = 2;

    public void Start()
    {
        Button.onClick.AddListener(PressButton);
    }

    public void PressButton()
    {
        StartCoroutine(Sign());
    }

    private IEnumerator Sign()
    {
        ContextsContainer.GetContext<EventSystemContext>().EventSystem.enabled = false;
        while (MainMenuCanvasGroup.alpha > 0)
        {
            MainMenuCanvasGroup.alpha -= Time.deltaTime * MainMenuFadeSpeed;
            yield return null;
        }
        MainMenuCanvas.gameObject.SetActive(false);
        SignCanvas.gameObject.SetActive(true);
        while (SignCanvasGroup.alpha < 1)
        {
            SignCanvasGroup.alpha += Time.deltaTime * SignFadeSpeed;
            yield return null;
        }
        ContextsContainer.GetContext<EventSystemContext>().EventSystem.enabled = true;
        yield return new WaitForSeconds(waitForSeconds);
        ContextsContainer.GetContext<SceneLoaderContext>().LoadScene(NextScene);
    }
}