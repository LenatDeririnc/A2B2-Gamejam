using Movement;
using UnityEngine;

namespace SystemInitializer.Systems
{
    public class Character : MonoBehaviour
    {
        public SpriteRenderer Sprite;
        public SpeechAction Speech;
        public string Sequence;
        public bool skip;
    }
}