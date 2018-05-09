using System.Collections.Generic;
using System;

namespace LSTools
{
    public abstract class ABaseEventBinder
    {
        protected struct ABinding
        {
            public ABaseEvent Event;
            public object Action;

            public ABinding(ABaseEvent e, object action)
            {
                Event = e;
                Action = action;
            }
        }

        protected readonly HashSet<ABinding> m_Bindings = new HashSet<ABinding>();

        protected void HandleBind(ABinding binding)
        {
            if (binding.Event != null && !m_Bindings.Contains(binding))
            {
                m_Bindings.Add(binding);
                binding.Event.AddListener(binding.Action);
            }
        }

        protected void HandleUnbind(ABinding binding)
        {
            if (m_Bindings.Remove(binding))
            {
                binding.Event.RemoveListener(binding.Action);
            }
        }

        public void Unbind()
        {
            foreach (ABinding binding in m_Bindings)
            {
                binding.Event.RemoveListener(binding.Action);
            }

            m_Bindings.Clear();
        }
    }

    public class AEventBinder : ABaseEventBinder
    {
        public void Bind(AEvent e, Action action)
        {
            HandleBind(new ABinding(e, action));
        }

        public void Unbind(AEvent e, Action action)
        {
            HandleUnbind(new ABinding(e, action));
        }

        public void Bind<T>(AEvent<T> e, Action action)
        {
            HandleBind(new ABinding(e, action));
        }

        public void Bind<T>(AEvent<T> e, Action<T> action)
        {
            HandleBind(new ABinding(e, action));
        }

        public void Unbind<T>(AEvent<T> e, Action action)
        {
            HandleUnbind(new ABinding(e, action));
        }

        public void Unbind<T>(AEvent<T> e, Action<T> action)
        {
            HandleUnbind(new ABinding(e, action));
        }

        public void Bind<T1, T2>(AEvent<T1, T2> e, Action action)
        {
            HandleBind(new ABinding(e, action));
        }

        public void Bind<T1, T2>(AEvent<T1, T2> e, Action<T1> action)
        {
            HandleBind(new ABinding(e, action));
        }

        public void Bind<T1, T2>(AEvent<T1, T2> e, Action<T1, T2> action)
        {
            HandleBind(new ABinding(e, action));
        }

        public void Unbind<T1, T2>(AEvent<T1, T2> e, Action action)
        {
            HandleUnbind(new ABinding(e, action));
        }

        public void Unbind<T1, T2>(AEvent<T1, T2> e, Action<T1> action)
        {
            HandleUnbind(new ABinding(e, action));
        }

        public void Unbind<T1, T2>(AEvent<T1, T2> e, Action<T1, T2> action)
        {
            HandleUnbind(new ABinding(e, action));
        }
    }
}