using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

namespace LSTools
{
    // #TODO LS vertical carousel support
    // #TODO LS looping support
    // #TODO LS drag falloff curve
    public class UICarousel : AUIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private Transform m_CarouselDir;
        [SerializeField]
        private AnimationCurve m_WrapCurve = AnimationCurve.Linear(0, 1, 1, 0);

        public float Position { get; private set; }
        public float TargetPosition { get; private set; }
        public bool IsDirty { get; private set; }
        public bool IsDragging { get; private set; }

        [NonSerialized]
        protected List<AUICarouselElement> m_Elements = new List<AUICarouselElement>();

        [NonSerialized]
        protected float m_ElementWidth = DEFAULT_WIDTH;       // #TODO LS could be handled better?

        [NonSerialized]
        protected float m_DragStartPosition = 0f;
        [NonSerialized]
        protected float m_DragBuffer = 0f;

        protected const float DEFAULT_WIDTH = -1f;
        protected const float DRAG_ACCEL_THRESHOLD = 0.15f;      // if drag acceleration is higher than this on drag end, go to next item
        protected const float SCREEN_WRAP_DISTANCE = 0.5f;      // how many screen width sizes can we scroll past

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

        public void OnBeginDrag(PointerEventData data)
        {
            IsDragging = true;
            m_DragStartPosition = Position;
            m_DragBuffer = 0;
        }

        public void OnDrag(PointerEventData data)
        {
            float dx = data.delta.x / m_ElementWidth;
            float wrapTest = m_DragStartPosition + m_DragBuffer - dx;

            // if going past bounds, reduce acceleration
            if (wrapTest < 0)
            {
                dx *= m_WrapCurve.Evaluate(-wrapTest);
            }
            else if (wrapTest > m_Elements.Count - 1)
            {
                dx *= m_WrapCurve.Evaluate(wrapTest - (m_Elements.Count - 1));
            }

            // apply translation
            m_DragBuffer -= dx;
            SetPosition(m_DragStartPosition + m_DragBuffer);
        }

        // using some stuff from https://github.com/taka-oyama/ScrollSnap/blob/master/Assets/ScrollSnap.cs
        public void OnEndDrag(PointerEventData data)
        {
            // check if we should snap to next item
            float dx = data.delta.x / m_ElementWidth;
            float acceleration = Mathf.Abs(dx / Time.deltaTime);
            if (acceleration > DRAG_ACCEL_THRESHOLD && acceleration != Mathf.Infinity)
            {
                float pos = Position;
                if (dx > 0)
                {
                    pos = Mathf.FloorToInt(pos);
                }
                else
                {
                    pos = Mathf.CeilToInt(pos);
                }
                SetTargetPosition(pos);

                m_DragStartPosition = pos;
                m_DragBuffer = 0f;
            }
            // otherwise round to nearest
            else
            {
                float newPos = Mathf.RoundToInt(Position);
                SetTargetPosition(newPos);
            }

            IsDragging = false;
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
                    rt.localScale = Vector2.one;        // necessary for GP's level select, but could be an option?

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
                Position = Mathf.Clamp(pos, -SCREEN_WRAP_DISTANCE, m_Elements.Count - 1 + SCREEN_WRAP_DISTANCE);
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
            TargetPosition = Mathf.Clamp(target, 0, m_Elements.Count - 1);
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
            if (!IsDragging)
            {
                float newPos = Mathf.Lerp(Position, TargetPosition, 0.4f);      // #TODO LS fix me
                SetPosition(newPos);
            }
        }
    }
}