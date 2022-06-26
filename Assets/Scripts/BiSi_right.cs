using System.Collections;
using SceneManager;
using SceneManager.ScriptableObjects;
using SystemInitializer;
using SystemInitializer.Systems;
using UnityEngine;
using UnityEngine.UI;

public class BiSi_right : MonoBehaviour
{
    public SceneLink NextScene;
    private MiniGamesContext MiniGamesContext => ContextsContainer.GetContext<MiniGamesContext>();
    private BiSiContext BiSiContext => ContextsContainer.GetContext<BiSiContext>();
    private CharactersContext CharactersContext => ContextsContainer.GetContext<CharactersContext>();
    
    public BiSiButton redButton;
    public BiSiButton greenButton;
    public BiSiButton blueButton;

    public string inputSequence;

    public GameObject CanvasMainBeforeMiniGames;
    public GameObject CanvasMainInput;
    public GameObject CanvasMainSequenceIsReady;
    public GameObject CanvasWaitingOtherInput;

    public GameObject CanvasErrorMiniGame;
    public GameObject CanvasErrorSequence;

    private GameObject CurrentCanvasMain;
    private GameObject CurrentCanvasError;

    private Coroutine waitCoroutine;

    public Image[] ColorSequence;

    public bool IsWaitingInput = false;

    public void Awake()
    {
        redButton.Action += RedButtonAction;
        greenButton.Action += GreenButtonAction;
        blueButton.Action += BlueButtonAction;

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

    public void SetReadyForInput()
    {
        ReplaceCurrentCanvas(CanvasMainInput);
    }

    private void EndAction()
    {
        Debug.Log("EndAction");
        // ContextsContainer.GetContext<SceneLoaderContext>().LoadScene(NextScene);
    }

    private void MiniGamesErrorView()
    {
        Debug.Log("MiniGamesErrorView");
        ReplaceCurrentErrorCanvas(CanvasErrorMiniGame);
        waitCoroutine = StartCoroutine(Error());
    }
    
    private void WaitingOtherInputErrorView()
    {
        Debug.Log("WaitingOtherInputErrorView");
        ReplaceCurrentErrorCanvas(CanvasWaitingOtherInput);
        waitCoroutine = StartCoroutine(Error());
    }

    private void SequenceSequenceErrorView()
    {
        Debug.Log("SequenceErrorView");
        ReplaceCurrentErrorCanvas(CanvasErrorSequence);
        waitCoroutine = StartCoroutine(Error());
    }

    private void SequenceSequenceSuccessView()
    {
        Debug.Log("SequenceSuccessView");
        ReplaceCurrentCanvas(CanvasMainSequenceIsReady);
        CharactersContext.CurrentCharacter().Speech.UpdateDialogue();
        BiSiContext.SetLeftActive();
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

    private void ClearInput()
    {
        inputSequence = "";
        foreach (var color in ColorSequence)
        {
            color.color = Color.white;
        }
    }
    
    private void CheckResult()
    {
        if (inputSequence.Length < 5)
            return;

        if (BiSiContext.biSiLeft.CheckResult(inputSequence))
        {
            if (BiSiContext.biSiLeft.IsDone())
            {
                EndAction();
            }
            SequenceSequenceSuccessView();
        }
        else
        {
            SequenceSequenceErrorView();
        }
        
        ClearInput();
    }

    private bool IsInputFailed()
    {
        if (waitCoroutine != null)
            return true;
        
        if (!MiniGamesContext.IsMiniGamesDone())
        {
            MiniGamesErrorView();
            return true;
        }

        if (BiSiContext.biSiLeft.IsWaitingInput)
        {
            WaitingOtherInputErrorView();
            return true;
        }

        return false;
    }

    private void RedButtonAction()
    {
        if (IsInputFailed()) 
            return;
        ColorSequence[inputSequence.Length].color = Color.red;
        inputSequence += "1";
        CheckResult();
    }


    private void GreenButtonAction()
    {
        if (IsInputFailed()) 
            return;
        ColorSequence[inputSequence.Length].color = Color.green;
        inputSequence += "2";
        CheckResult();
    }

    private void BlueButtonAction()
    {
        if (IsInputFailed()) 
            return;
        ColorSequence[inputSequence.Length].color = Color.blue;
        inputSequence += "3";
        CheckResult();
    }
}