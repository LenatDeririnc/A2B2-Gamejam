using Fungus;
using UnityEngine.Events;

namespace Movement
{
    public class SpeechAction : ButtonAction
    {
        public Flowchart Flowchart;
        public string StartBlockName = "Start";

        public UnityEvent EventAfterSpeech;

        public override void Execute()
        {
            Flowchart.ExecuteBlock(StartBlockName);
        }
    }
}