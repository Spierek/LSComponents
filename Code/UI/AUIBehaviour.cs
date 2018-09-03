using UnityEngine;

namespace LSTools
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class AUIBehaviour : CachedMonoBehaviour
    {
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
                Hide(true);
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

        public void Show(bool force = false)
        {
            m_Group.blocksRaycasts = true;

            m_TargetAlpha = 1;
            if (!m_UseFade || force)
            {
                SetAlpha(1);
            }

            HandleShow();
        }

        protected virtual void HandleShow()
        {
        }

        public void Hide(bool force = false)
        {
            m_Group.blocksRaycasts = false;

            m_TargetAlpha = 0;
            if (!m_UseFade || force)
            {
                SetAlpha(0);
            }

            HandleHide();
        }

        protected virtual void HandleHide()
        {
        }

        private void UpdateVisibility()
        {
            if (m_TargetAlpha != m_Group.alpha)
            {
                float sign = Mathf.Sign(m_TargetAlpha - m_Group.alpha);
                float duration = sign > 0 ? m_FadeInDuration : m_FadeOutDuration;
                duration += float.Epsilon;      // to avoid division by 0

                float mod = sign * (Time.deltaTime / duration);
                float alpha = m_Group.alpha + mod;
                m_Group.alpha = alpha;
                m_Group.alpha = Mathf.Clamp01(m_Group.alpha);
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
    }
}