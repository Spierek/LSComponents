using UnityEngine;
using System.Collections.Generic;

namespace LSTools
{
    public class AComponentPool : CachedMonoBehaviour
    {
        [SerializeField]
        private APoolable m_Template;
        [SerializeField]
        private int m_InitialCount = 4;

        private List<APoolable> m_ActiveObjects = new List<APoolable>();
        private Stack<APoolable> m_InactiveObjects = new Stack<APoolable>();

        public void Initialize()
        {
            for (int i = 0; i < m_InitialCount; ++i)
            {
                Create();
            }
        }

        public APoolable Spawn()
        {
            if (m_InactiveObjects.Count <= 0)
            {
                Create();
            }

            if (m_InactiveObjects.Count <= 0)
            {
                return null;
            }

            APoolable obj = m_InactiveObjects.Pop();
            obj.Spawn();
            m_ActiveObjects.Add(obj);
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Despawn(APoolable obj)
        {
            // #TODO LS this is kinda slow, need something faster
            if (m_ActiveObjects.Contains(obj))
            {
                m_ActiveObjects.Remove(obj);
                obj.gameObject.SetActive(false);
                obj.PoolDespawn();
                m_InactiveObjects.Push(obj);
            }
        }

        public void ForceDespawnAll()
        {
            for (int i = 0; i < m_ActiveObjects.Count; ++i)
            {
                APoolable obj = m_ActiveObjects[i];
                obj.gameObject.SetActive(false);
                obj.PoolDespawn();
                m_InactiveObjects.Push(obj);
            }

            m_ActiveObjects.Clear();
        }

        private void Create()
        {
            if (m_Template != null)
            {
                APoolable obj = Instantiate(m_Template, CachedTransform);
               obj.gameObject.SetActive(false);
                obj.Create(this);
               m_InactiveObjects.Push(obj);
            }
        }
    }
}