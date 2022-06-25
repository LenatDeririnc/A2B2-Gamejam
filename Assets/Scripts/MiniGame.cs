using Movement;

public class MiniGame : ButtonAction
{
    public MovementPoint point;
    public bool isDone;

    public override void Execute()
    {
        point.Execute();
        isDone = true;
    }
}