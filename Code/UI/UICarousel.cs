﻿using UnityEngine;
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
        public readonly AEvent<float> OnDragPositionUpdated = new AEvent<float>();
        public readonly AEvent OnDragFinished = new AEvent();

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
        protected const float DRAG_ACCEL_THRESHOLD = 0.12f;      // if drag acceleration is higher than this on drag end, go to next item
        protected const float SCREEN_WRAP_DISTANCE = 0.5f;      // how many screen width sizes can we scroll past

        public override void Initialize()
        {
            base.Initialize();
            ForcePosition(0);
        }

        protected void LateUpdate()
        {
            CheckElementWidth();
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
            float newPos = m_DragStartPosition + m_DragBuffer;
            SetPosition(m_DragStartPosition + m_DragBuffer);
            OnDragPositionUpdated.Invoke(newPos);
        }

        // using some stuff from https://github.com/taka-oyama/ScrollSnap/blob/master/Assets/ScrollSnap.cs
        public void OnEndDrag(PointerEventData data)
        {
            float newPos = Position;

            // check if we should snap to next item
            float dx = data.delta.x / m_ElementWidth;
            float acceleration = Mathf.Abs(dx / Time.deltaTime);
            if (acceleration > DRAG_ACCEL_THRESHOLD && acceleration != Mathf.Infinity)
            {
                if (dx > 0)
                {
                    newPos = Mathf.FloorToInt(newPos);
                }
                else
                {
                    newPos = Mathf.CeilToInt(newPos);
                }
            }
            // otherwise round to nearest
            else
            {
                newPos = Mathf.RoundToInt(newPos);
            }

            SetTargetPosition(newPos);
            OnDragPositionUpdated.Invoke(newPos);

            m_DragStartPosition = newPos;
            m_DragBuffer = 0f;
            IsDragging = false;

            OnDragFinished.Invoke();
        }

        public void AddElement(AUICarouselElement element)
        {
            if (element != null)
            {
                m_Elements.Add(element);
                element.CachedTransform.SetParent(m_CarouselDir);

                RectTransform rt = element.RectTransform;
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

        // #HACK LS checks every frame if default width has not changed and sets dirty flag if necessary
        private void CheckElementWidth()
        {
            if (m_Elements.Count > 0)
            {
                AUICarouselElement element = m_Elements[0];
                if (element != null)
                {
                    RectTransform rt = element.RectTransform;
                    if (rt != null && m_ElementWidth != rt.rect.width)
                    {
                        m_ElementWidth = rt.rect.width;
                        IsDirty = true;
                        SetPosition(0);
                    }
                }
            }
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