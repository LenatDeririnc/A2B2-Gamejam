using System;
using System.Collections.Generic;
using AS.SystemInitializer.Interfaces;

namespace AS.SystemInitializer
{
    public static class ContextsContainer
    {
        private static readonly Dictionary<Type, MonoBehaviourContext> _contexts = new Dictionary<Type, MonoBehaviourContext>();
        
        public static void Initialize(List<MonoBehaviourContext> contexts)
        {
            foreach (var context in contexts)
            {
                var type = context.GetType();
                _contexts[type] = context;
                context.SystemInit();
            }
        }

        public static void AddContext<T>(T context)
        {
            _contexts[typeof(T)] = context as MonoBehaviourContext;
        }

        public static T GetContext<T>() where T : MonoBehaviourContext
	    {
            if (_contexts.ContainsKey(typeof(T)))
		        return (T) _contexts[typeof(T)];
            return null;
        }
    }
}