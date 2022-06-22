using System;
using UnityEngine;

namespace SystemInitializer
{
    public abstract class SimpleSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T Instance;

        protected void InitSingleton(T instance, Action firstInitAction = null)
        {
            if (Instance == null)
            {
                Instance = instance;
                DontDestroyOnLoad(gameObject);
                firstInitAction?.Invoke();
            }
            else
            {
                Destroy(this);
                Destroy(gameObject);
            }
        }
    }
}