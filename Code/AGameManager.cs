using UnityEngine;
using System.Collections.Generic;

namespace LSTools
{
    // entry point for the entire game, should hold all relevant references and always be there for you
    public abstract class AGameManager : ASISingleton<AGameManager>
    {
        protected List<AModule> m_InitializedModules = new List<AModule>();

        protected readonly AEventBinder m_Binder = new AEventBinder();
        protected virtual bool m_InitializeOnAwake { get { return true; } }

        protected override void Awake()
        {
            base.Awake();

            if (m_InitializeOnAwake)
            {
                Initialize();
            }
        }

        protected override void HandleInitialization()
        {
            base.HandleInitialization();
        }

        protected override void HandleUninitialization()
        {
            base.HandleUninitialization();
        }

        protected void AddModule(AModule module)
        {
            if (module != null)
            {
                module.Initialize();
                m_InitializedModules.Add(module);
            }
        }

        private void RemoveModule(AModule module, bool isClearing = false)
        {
            if (module != null)
            {
                module.Uninitialize();
                if (!isClearing)
                {
                    m_InitializedModules.Remove(module);
                }
            }
        }

        // removes all modules in reverse order
        private void ClearModules()
        {
            for (int i = m_InitializedModules.Count - 1; i >= 0; --i)
            {
                AModule module = m_InitializedModules[i];
                if (module != null)
                {
                    RemoveModule(module, true);
                }
            }

            m_InitializedModules.Clear();
        }
    }
}