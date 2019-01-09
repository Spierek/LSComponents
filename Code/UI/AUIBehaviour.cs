using System;
using UnityEngine;

namespace LSTools
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class AUIBehaviour : CachedMonoBehaviour
    {
        public readonly AEvent<AUIBehaviour> OnShown = new AEvent<AUIBehaviour>();
        public readonly AEvent<AUIBehaviour> OnHidden = new AEvent<AUIBehaviour>();

        [SerializeField]
        protected CanvasGroup m_Group;

        [Space]
        [SerializeField]
        protected bool m_UseFade = false;
        [SerializeField, Range(0f, 5f)]
        protected float m_FadeInDuration = 1f;
        [SerializeField, Range(0f, 5f)]
        protected float m_FadeOutDuration = 1f;

        private float m_TargetAlpha = 1f;

        [NonSerialized]
        private RectTransform m_RectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (m_RectTransform == null)
                {
                    m_RectTransform = transform as RectTransform;
                }
                return m_RectTransform;
            }
        }

        protected virtual bool m_InitializeVisible { get { return true; } }

        public bool IsVisible { get { return m_Group.alpha > 0f; } }

        protected virtual void Awake()
        {
            FindReferences();
        }

        protected virtual void Update()
        {
            if (m_UseFade)
            {
                UpdateVisibility();
            }
        }

        public virtual void Initialize()
        {
            // force hidden position, then fade in properly
            if (m_UseFade && m_InitializeVisible)
            {
                Hide(true, false);
            }

            SetVisibility(m_InitializeVisible);
        }

        public virtual void Uninitialize()
        {
            Hide();
        }

        public void SetVisibility(bool set, bool force = false)
        {
            if (set)
            {
                Show(force);
            }
            else
            {
                Hide(force);
            }
        }

        public void Show(bool force = false, bool sendEvent = true)
        {
            m_Group.blocksRaycasts = true;
            m_Group.interactable = true;

            HandleShow();

            m_TargetAlpha = 1;
            if (!m_UseFade || force)
            {
                SetAlpha(1);
                if (sendEvent)
                {
                    OnShown.Invoke(this);
                }
            }
        }

        protected virtual void HandleShow()
        {
        }

        public void Hide(bool force = false, bool sendEvent = true)
        {
            m_Group.blocksRaycasts = false;
            m_Group.interactable = false;

            HandleHide();

            m_TargetAlpha = 0;
            if (!m_UseFade || force)
            {
                SetAlpha(0);
                if (sendEvent)
                {
                    OnHidden.Invoke(this);
                }
            }
        }

        protected virtual void HandleHide()
        {
        }

        private void UpdateVisibility()
        {
            if (m_Group.alpha != m_TargetAlpha)
            {
                float sign = Mathf.Sign(m_TargetAlpha - m_Group.alpha);
                float duration = sign > 0 ? m_FadeInDuration : m_FadeOutDuration;
                duration += float.Epsilon;      // to avoid division by 0

                float mod = sign * (Time.deltaTime / duration);
                float alpha = m_Group.alpha + mod;
                m_Group.alpha = alpha;
                m_Group.alpha = Mathf.Clamp01(m_Group.alpha);

                if (m_Group.alpha == m_TargetAlpha)
                {
                    if (m_Group.alpha == 1)
                    {
                        OnShown.Invoke(this);
                    }
                    else if (m_Group.alpha == 0)
                    {
                        OnHidden.Invoke(this);
                    }
                }
            }
        }

        private void SetAlpha(float alpha)
        {
            m_Group.alpha = alpha;
        }

        private void OnValidate()
        {
            FindReferences();
        }

        protected virtual void FindReferences()
        {
            if (m_Group == null)
            {
                m_Group = GetComponent<CanvasGroup>();
            }
        }

#if UNITY_EDITOR
        public void Editor_SetVisibility(bool set)
        {
            SetAlpha(set ? 1 : 0);
        }
#endif
    }
}