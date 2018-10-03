using System;
using UnityEngine;

namespace LSTools
{
    public abstract class CachedMonoBehaviour : MonoBehaviour
    {
        [NonSerialized]
        private Transform m_CachedTransform;
        public Transform CachedTransform
        {
            get
            {
                if (m_CachedTransform == null)
                {
                    m_CachedTransform = transform;
                }
                return m_CachedTransform;
            }
        }
    }
}