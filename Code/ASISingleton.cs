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
                }

                return m_Instance;
            }
        }

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

            DontDestroyOnLoad(gameObject);
        }
    }
}