using System;
using System.Collections.Generic;

namespace LSTools
{
    public abstract class ABaseEvent
    {
        private readonly HashSet<object> m_Actions = new HashSet<object>();

        public void AddListener(object action)
        {
            m_Actions.Add(action);
        }

        public void RemoveListener(object action)
        {
            m_Actions.Remove(action);
        }

        public void ClearListeners()
        {
            m_Actions.Clear();
        }

        protected void Invoke(params object[] p)
        {
            List<object> currentActions = new List<object>(m_Actions);
            foreach (object action in currentActions)
            {
                HandleInvoke(action, p);
            }
        }

        protected abstract void HandleInvoke(object action, params object[] p);
    }

    public class AEvent : ABaseEvent
	{
		public void AddListener(Action action)
		{
            base.AddListener(action);
        }

		public void RemoveListener(Action action)
		{
            base.RemoveListener(action);
        }

        public void Invoke()
        {
            base.Invoke();
        }

        protected override void HandleInvoke(object action, params object[] p)
        {
            ((Action)action)();
        }
    }

    public class AEvent<T> : ABaseEvent
    {
        public void AddListener(Action<T> action)
        {
            base.AddListener(action);
        }

        public void RemoveListener(Action<T> action)
        {
            base.RemoveListener(action);
        }

        public void Invoke(T t)
        {
            base.Invoke(t);
        }

        protected override void HandleInvoke(object action, params object[] p)
        {
            ((Action<T>)action)((T)p[0]);
        }
    }

    public class AEvent<T1, T2> : ABaseEvent
    {
        public void AddListener(Action<T1, T2> action)
        {
            base.AddListener(action);
        }

        public void RemoveListener(Action<T1, T2> action)
        {
            base.RemoveListener(action);
        }

        public void Invoke(T1 t1, T2 t2)
        {
            base.Invoke(t1, t2);
        }

        protected override void HandleInvoke(object action, params object[] p)
        {
            ((Action<T1, T2>)action)((T1)p[0], (T2)p[1]);
        }
    }
}
