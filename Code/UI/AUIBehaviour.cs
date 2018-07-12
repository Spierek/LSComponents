using UnityEngine;

namespace LSTools
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class AUIBehaviour : CachedMonoBehaviour
    {
        [SerializeField]
        protected CanvasGroup m_Group;

        protected virtual bool m_InitializeVisible { get { return true; } }

        public bool IsVisible { get; private set; }

        protected virtual void Awake()
        {
            FindReferences();
        }

        public virtual void Initialize()
        {
            SetVisibility(m_InitializeVisible);
        }

        public virtual void Uninitialize()
        {
            Hide();
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
            IsVisible = true;
            m_Group.alpha = 1;
            HandleShow();
        }

        protected virtual void HandleShow() { }

        public void Hide()
        {
            IsVisible = false;
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