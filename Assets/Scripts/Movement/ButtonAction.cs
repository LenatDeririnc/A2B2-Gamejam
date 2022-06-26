using UnityEngine;

namespace Movement
{
    public abstract class ButtonAction : MonoBehaviour
    {
        public abstract void Execute();

        public virtual bool IsEnabled()
        {
            return true;
        }
    }
}