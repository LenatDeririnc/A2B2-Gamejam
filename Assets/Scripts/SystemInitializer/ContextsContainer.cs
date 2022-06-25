using System;
using System.Collections.Generic;

namespace SystemInitializer
{
    public static class ContextsContainer
    {
        private static Dictionary<Type, MonoBehaviourContext> _contexts;
        
        public static void Initialize(List<MonoBehaviourContext> contexts)
        {
            _contexts = new Dictionary<Type, MonoBehaviourContext>();
            foreach (var context in contexts)
            {
                var type = context.GetType();
                _contexts[type] = context;
            }
        }

        public static void OverrideContexts(List<MonoBehaviourContext> contexts)
        {
            foreach (var context in contexts)
            {
                var type = context.GetType();
                _contexts[type] = context;
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