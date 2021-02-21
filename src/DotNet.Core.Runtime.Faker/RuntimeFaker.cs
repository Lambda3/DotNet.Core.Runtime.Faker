using System;
using System.Collections.Generic;

namespace DotNet.Core.Runtime.Faker
{
    internal class RuntimeFaker
    {
        private readonly Dictionary<Type, object> registeredFakers = new Dictionary<Type, object>();
        private Dictionary<Type, object> changedFakers = new Dictionary<Type, object>();

        public virtual T Get<T>() where T : class
        {
            var fakers = registeredFakers;
            if (changedFakers.ContainsKey(typeof(T)))
                fakers = changedFakers;

            return fakers.ContainsKey(typeof(T)) ? (T)fakers[typeof(T)] : default;
        }

        public virtual void Register<T>(T faker) where T : class
        {
            Remove<T>(registeredFakers);
            registeredFakers.Add(typeof(T), faker);
        }

        public void Change<T>(T faker) where T : class
        {
            if (!Contains<T>(registeredFakers))
                throw new InvalidOperationException($"There is no fake registered from type {typeof(T)}");

            Remove<T>(changedFakers);
            changedFakers.Add(typeof(T), faker);
        }

        public void ResetChange<T>() where T : class => Remove<T>(changedFakers);

        public void ResetAllChanges() => changedFakers = new Dictionary<Type, object>();

        protected void Remove<T>(Dictionary<Type, object> fakers) where T : class
        {
            if (Contains<T>(fakers))
                fakers.Remove(typeof(T));
        }

        private bool Contains<T>(Dictionary<Type, object> fakers) where T : class => fakers.ContainsKey(typeof(T));
    }
}
