using SystemInitializer;

public class MiniGamesContext : MonoBehaviourContext
{
    public MiniGame MiniGame1;
    public MiniGame MiniGame2;
    public MiniGame MiniGame3;
    
    public bool IsMiniGamesDone()
    {
        return MiniGame1.isDone && MiniGame2.isDone && MiniGame3.isDone;
    }
}