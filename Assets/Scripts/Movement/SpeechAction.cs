using Fungus;
using UnityEngine.Events;

namespace Movement
{
    public class SpeechAction : ButtonAction
    {
        public Flowchart Flowchart;
        public string FirstDialogue = "FistDialogue";
        public string SecondDialogue = "SecondDialogue";

        private string currentDialogue;

        public UnityEvent EventAfterSpeech;

        private void Awake()
        {
            currentDialogue = FirstDialogue;
        }

        public override void Execute()
        {
            Flowchart.ExecuteBlock(currentDialogue);
        }

        public void UpdateDialogue()
        {
            currentDialogue = SecondDialogue;
        }
    }
}