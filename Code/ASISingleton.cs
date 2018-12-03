using UnityEngine;

namespace LSTools
{
    // self instantiating singleton
    public abstract class ASISingleton<T> : CachedMonoBehaviour
        where T : ASISingleton<T>
    {
        private static T m_Instance;
        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    T inst = new GameObject(string.Format("[{0}]", typeof(T))).AddComponent<T>();
                    m_Instance = inst;
                    m_Instance.Initialize();
                }

                return m_Instance;
            }
        }

        protected virtual bool m_DontDestroyOnLoad { get { return true; } }
        public bool IsInitialized { get; private set; }

        protected virtual void Awake()
        {
            if (m_Instance == null)
            {
                m_Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }

            if (m_DontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnDestroy()
        {
            Uninitialize();
        }

        protected void Initialize()
        {
            if (!IsInitialized)
            {
                HandleInitialization();
                IsInitialized = true;
            }
        }

        protected virtual void HandleInitialization()
        {
        }

        protected void Uninitialize()
        {
            if (IsInitialized)
            {
                HandleUninitialization();
                IsInitialized = false;
            }
        }

        protected virtual void HandleUninitialization()
        {
        }
    }
}