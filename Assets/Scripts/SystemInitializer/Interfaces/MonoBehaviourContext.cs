using UnityEngine;

namespace AS.SystemInitializer.Interfaces
{
    public abstract class MonoBehaviourContext : MonoBehaviour
    {
        public virtual void Start()
        {
        }

        public virtual void SystemInit()
        {
        }
    }
}