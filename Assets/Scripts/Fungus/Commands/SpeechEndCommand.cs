using Movement;
using UnityEngine;

namespace Fungus.Commands
{
    [CommandInfo("Custom", "Speech End", "Speech End")]
    public class SpeechEndCommand : Command
    {
        [SerializeField] private SpeechAction Speech;
        public override void OnEnter()
        {
            Speech.EventAfterSpeech.Invoke();
            Continue();
        }
    }
}