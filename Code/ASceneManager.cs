﻿using System.Collections.Generic;

namespace LSTools
{
    // should keep all references in a given scene and initialize all modules in given order
    public abstract class ASceneManager : CachedMonoBehaviour
    {
        public readonly AEvent OnInitialized = new AEvent();

        public bool IsInitialized { get; private set; }

        protected List<AModule> m_InitializedModules = new List<AModule>();

        public void Initialize()
        {
            if (!IsInitialized)
            {
                HandleInitialization();

                IsInitialized = true;
                OnInitialized.Invoke();
            }
        }

        public void Uninitialize()
        {
            if (IsInitialized)
            {
                ClearModules();
                HandleUninitialization();
                IsInitialized = false;
            }
        }

        protected abstract void HandleInitialization();
        protected abstract void HandleUninitialization();

        protected void AddModule(AModule module)
        {
            if (module != null)
            {
                module.Initialize();
                m_InitializedModules.Add(module);
            }
        }

        private void RemoveModule(AModule module)
        {
            module.Uninitialize();
        }

        // removes all modules in reverse order
        private void ClearModules()
        {
            for (int i = m_InitializedModules.Count - 1; i >= 0; --i)
            {
                AModule module = m_InitializedModules[i];
                if (module != null)
                {
                    RemoveModule(module);
                }
            }

            m_InitializedModules.Clear();
        }
    }
}