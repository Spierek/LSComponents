using UnityEngine;

namespace LSTools
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class AUIBehaviour : CachedMonoBehaviour
    {
        [SerializeField]
        protected CanvasGroup m_Group;

        protected virtual bool m_InitializeVisible { get { return true; } }

        protected virtual void Awake()
        {
            FindReferences();
        }

        public virtual void Initialize()
        {
            SetVisibility(m_InitializeVisible);
        }

        public void SetVisibility(bool set)
        {
            if (set)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void Show()
        {
            m_Group.alpha = 1;
            HandleShow();
        }

        protected virtual void HandleShow() { }

        public void Hide()
        {
            m_Group.alpha = 0;
            HandleHide();
        }

        protected virtual void HandleHide() { }

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