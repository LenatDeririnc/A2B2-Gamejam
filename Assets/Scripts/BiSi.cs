using System;
using System.Collections;
using SceneManager;
using SceneManager.ScriptableObjects;
using SystemInitializer;
using UnityEngine;
using UnityEngine.UI;

public class BiSi : MonoBehaviour
{
    public SceneLink NextScene;
    public MiniGamesContext MiniGamesContext => ContextsContainer.GetContext<MiniGamesContext>();
    
    public BiSiButton redButton;
    public BiSiButton greenButton;
    public BiSiButton blueButton;

    public int currentSequence = 0;
    public string[] sequences;

    public string inputSequence;

    public GameObject CanvasBeforeMiniGames;
    public GameObject CanvasAfterMiniGames;
    public GameObject CanvasError;

    private GameObject CurrentCanvas;

    private Coroutine waitCoroutine;

    public Image[] ColorSequence;

    public void Awake()
    {
        redButton.Action += RedButtonAction;
        greenButton.Action += GreenButtonAction;
        blueButton.Action += BlueButtonAction;

        CurrentCanvas = CanvasBeforeMiniGames;
    }

    public void SetReadyForInput()
    {
        CanvasBeforeMiniGames.SetActive(false);
        CurrentCanvas = CanvasAfterMiniGames;
        CanvasAfterMiniGames.SetActive(true);
    }

    public bool IsMiniGamesDone()
    {
        return MiniGamesContext.MiniGame1.isDone && MiniGamesContext.MiniGame2.isDone && MiniGamesContext.MiniGame3.isDone;
    }

    public void EndAction()
    {
        Debug.Log("EndAction");
        ContextsContainer.GetContext<SceneLoaderContext>().LoadScene(NextScene);
    }

    public void MiniGamesErrorView()
    {
        Debug.Log("MiniGamesErrorView");
        waitCoroutine = StartCoroutine(Error());
    }

    public void SequenceErrorView()
    {
        Debug.Log("SequenceErrorView");
        waitCoroutine = StartCoroutine(Error());
    }

    private IEnumerator Error()
    {
        CurrentCanvas.SetActive(false);
        CanvasError.SetActive(true);
        yield return new WaitForSeconds(1f);
        CanvasError.SetActive(false);
        CurrentCanvas.SetActive(true);
        waitCoroutine = null;
    }

    public void SequenceSuccessView()
    {
        Debug.Log("SequenceSuccessView");
    }

    private void NextSequence()
    {
        if (currentSequence < 2)
        {
            currentSequence += 1;
        }
        else
        {
            EndAction();
        }
    }

    private void ClearInput()
    {
        inputSequence = "";
        foreach (var color in ColorSequence)
        {
            color.color = Color.white;
        }
    }

    public void CheckResult()
    {
        if (sequences[currentSequence] == inputSequence)
        {
            SequenceSuccessView();
            NextSequence();
        }
        else
        {
            SequenceErrorView();
        }

        ClearInput();
    }

    private void RedButtonAction()
    {
        if (waitCoroutine != null)
            return;
        if (!IsMiniGamesDone())
        {
            MiniGamesErrorView();
            return;
        }
        ColorSequence[inputSequence.Length].color = Color.red;
        inputSequence += "1";
        if (inputSequence.Length >= 5)
            CheckResult();
    }
    

    private void GreenButtonAction()
    {
        if (waitCoroutine != null)
            return;
        if (!IsMiniGamesDone())
        {
            MiniGamesErrorView();
            return;
        }
        ColorSequence[inputSequence.Length].color = Color.green;
        inputSequence += "2";
        if (inputSequence.Length >= 5)
            CheckResult();
    }

    private void BlueButtonAction()
    {
        if (waitCoroutine != null)
            return;
        if (!IsMiniGamesDone())
        {
            MiniGamesErrorView();
            return;
        }
        ColorSequence[inputSequence.Length].color = Color.blue;
        inputSequence += "3";
        if (inputSequence.Length >= 5)
            CheckResult();
    }
}