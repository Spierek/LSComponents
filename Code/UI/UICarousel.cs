using UnityEngine;
using System.Collections.Generic;
using System;

namespace LSTools
{
    // #TODO LS vertical carousel support
    // #TODO LS looping support
    public class UICarousel : AUIBehaviour
    {
        [SerializeField]
        private Transform m_CarouselDir;

        public float Position { get; private set; }
        public float TargetPosition { get; private set; }
        public bool IsDirty { get; private set; }

        [NonSerialized]
        protected List<AUICarouselElement> m_Elements = new List<AUICarouselElement>();

        [NonSerialized]
        protected float m_ElementWidth = DEFAULT_WIDTH;       // #TODO LS could be handled better?

        protected const float DEFAULT_WIDTH = -1f;

        public override void Initialize()
        {
            base.Initialize();
            ForcePosition(0);
        }

        protected void LateUpdate()
        {
            if (IsDirty)
            {
                RepositionElements();
            }
            UpdatePosition();
        }

        public void AddElement(AUICarouselElement element)
        {
            if (element != null)
            {
                m_Elements.Add(element);
                element.CachedTransform.SetParent(m_CarouselDir);

                RectTransform rt = element.CachedTransform as RectTransform;
                if (rt != null)
                {
                    rt.localPosition = Vector3.zero;
                    rt.sizeDelta = Vector2.zero;

                    if (m_ElementWidth == DEFAULT_WIDTH)
                    {
                        m_ElementWidth = rt.rect.width;
                    }
                }

                IsDirty = true;
            }
        }

        public void RemoveElement(AUICarouselElement element)
        {
            if (m_Elements.Contains(element))
            {
                m_Elements.Remove(element);
                element.CachedTransform.SetParent(null);

                IsDirty = true;
            }
        }

        public void SetPosition(float pos)
        {
            if (Position != pos)
            {
                Position = pos;
                float xOffset = -pos * m_ElementWidth;
                m_CarouselDir.localPosition = new Vector3(xOffset, m_CarouselDir.localPosition.y, m_CarouselDir.localPosition.z);
            }
        }

        public void ForcePosition(float pos)
        {
            if (IsDirty)
            {
                RepositionElements();
            }

            SetTargetPosition(pos);
            SetPosition(pos);
        }

        public void SetTargetPosition(float target)
        {
            TargetPosition = target;
        }

        private void RepositionElements()
        {
            for (int i = 0; i < m_Elements.Count; ++i)
            {
                AUICarouselElement element = m_Elements[i];
                if (element != null)
                {
                    float xOffset = i * m_ElementWidth;
                    element.CachedTransform.localPosition = new Vector3(xOffset, 0);
                }
            }

            IsDirty = false;
        }

        private void UpdatePosition()
        {
            float newPos = Mathf.Lerp(Position, TargetPosition, 0.5f);      // #TODO LS fix me
            SetPosition(newPos);
        }
    }
}