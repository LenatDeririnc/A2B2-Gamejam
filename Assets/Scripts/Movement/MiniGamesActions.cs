namespace Movement
{
    public class MiniGamesActions : ButtonAction
    {
        public MiniGame MG1;
        public MiniGame MG2;
        public MiniGame MG3;

        public override void Execute()
        {
            if (!MG1.isDone)
            {
                MG1.Execute();
                return;
            }

            if (!MG2.isDone)
            {
                MG2.Execute();
                return;
            }

            if (!MG3.isDone)
            {
                MG3.Execute();
                return;
            }
        }
    }
}