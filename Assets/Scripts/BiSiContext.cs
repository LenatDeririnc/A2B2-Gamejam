using SystemInitializer;

public class BiSiContext : MonoBehaviourContext
{
    public BiSi_right biSiRight;
    public BiSi_left biSiLeft;
    
    public void SetReadyForInput()
    {
        biSiRight.SetReadyForInput();
        biSiRight.SetReadyForInput();
        SetLeftActive();
    }

    public void SetLeftActive()
    {
        biSiLeft.IsWaitingInput = true;
        biSiLeft.ReplaceCurrentCanvas(biSiLeft.CanvasMainAfterMiniGames);
        biSiRight.IsWaitingInput = false;
        biSiRight.ReplaceCurrentCanvas(biSiRight.CanvasWaitingOtherInput);
    }
    
    public void SetRightActive()
    {
        biSiRight.IsWaitingInput = true;
        biSiRight.ReplaceCurrentCanvas(biSiRight.CanvasMainInput);
        biSiLeft.IsWaitingInput = false;
        biSiLeft.ReplaceCurrentCanvas(biSiLeft.CanvasMainWaitingOtherInput);
    }
}