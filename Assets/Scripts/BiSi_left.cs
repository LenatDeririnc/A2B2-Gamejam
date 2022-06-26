using System.Collections;
using SystemInitializer;
using UnityEngine;

public class BiSi_left : MonoBehaviour
{
    private MiniGamesContext MiniGamesContext => ContextsContainer.GetContext<MiniGamesContext>();
    private BiSiContext BiSiContext => ContextsContainer.GetContext<BiSiContext>();
    
    public int currentSequence = 0;
    public string[] sequences;
    
    public GameObject CanvasMainBeforeMiniGames;
    public GameObject CanvasMainAfterMiniGames;
    public GameObject CanvasMainWaitingOtherInput;
    
    public GameObject CanvasErrorMiniGame;
    public GameObject CanvasErrorButtonIsPressed;
    
    private GameObject CurrentCanvasMain;
    private GameObject CurrentCanvasError;
    
    private Coroutine waitCoroutine;
    
    public BiSiButton nextButton;
    
    public bool IsWaitingInput = false;
    
    public void Awake()
    {
        nextButton.Action += NextButtonAction;

        ReplaceCurrentCanvas(CanvasMainBeforeMiniGames);
        ReplaceCurrentErrorCanvas(CanvasErrorMiniGame);
    }

    public void ReplaceCurrentCanvas(GameObject newCanvas)
    {
        CurrentCanvasMain?.SetActive(false);
        CurrentCanvasMain = newCanvas;
        CurrentCanvasMain.SetActive(true);
    }
    
    private void ReplaceCurrentErrorCanvas(GameObject newCanvas)
    {
        CurrentCanvasError = newCanvas;
    }

    private void NextButtonAction()
    {
        if (waitCoroutine != null)
            return;

        if (!MiniGamesContext.IsMiniGamesDone())
        {
            Debug.Log("MiniGamesErrorView");
            ReplaceCurrentErrorCanvas(CanvasErrorMiniGame);
            PlayError();
            return;
        }

        if (BiSiContext.biSiRight.IsWaitingInput)
        {
            Debug.Log("WaitingOtherInputErrorView");
            ReplaceCurrentErrorCanvas(CanvasErrorButtonIsPressed);
            PlayError();
            return;
        }

        NextSequence();
    }

    private void PlayError()
    {
        waitCoroutine = StartCoroutine(Error());
    }

    private IEnumerator Error()
    {
        CurrentCanvasMain.SetActive(false);
        CurrentCanvasError.SetActive(true);
        yield return new WaitForSeconds(1f);
        CurrentCanvasError.SetActive(false);
        CurrentCanvasMain.SetActive(true);
        waitCoroutine = null;
    }
    
    private void NextSequence()
    {
        if (IsDone())
        {
            Debug.Log("It's done");
            PlayError();
            return;
        }
        
        currentSequence += 1;
        BiSiContext.SetRightActive();
    }
    
    public bool CheckResult(string result)
    {
        return sequences[currentSequence] == result;
    }

    public bool IsDone()
    {
        return currentSequence >= 2;
    }
    
    public void SetReadyForInput()
    {
        CanvasMainBeforeMiniGames.SetActive(false);
        CurrentCanvasMain = CanvasMainAfterMiniGames;
        CanvasMainAfterMiniGames.SetActive(true);
        IsWaitingInput = true;
    }

}