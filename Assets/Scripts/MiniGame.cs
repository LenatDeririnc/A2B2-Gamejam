using Movement;
using UnityEngine;
using UnityEngine.Events;

public class MiniGame : ButtonAction
{
    public Canvas DebugActionsCanvas;
    public MovementPoint point;
    public ButtonAction nextAction;
    public bool isDone;

    public UnityEvent AfterSuccessAction;

    public override void Execute()
    {
        if (isDone)
            return;
        
        MoveToPoint();
    }

    public void MoveToPoint()
    {
        void OnEnterAction()
        {
            DebugActionsCanvas.gameObject.SetActive(true);
        }

        void OnExitAction()
        {
            DebugActionsCanvas.gameObject.SetActive(false);
        }

        point.Execute(OnEnterAction, OnExitAction);
    }

    public void DoTask()
    {
        isDone = true;
        nextAction.Execute();
        AfterSuccessAction.Invoke();
    }
}