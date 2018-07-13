using UnityEngine;
using UnityEngine.UI;

namespace LSTools
{
    public class UIButton : AUIBehaviour
    {
        public readonly AEvent OnClicked = new AEvent();

        [SerializeField]
        protected Button m_Button;

        protected override void Awake()
        {
            base.Awake();
            m_Button.onClick.AddListener(HandleButtonClicked);
        }

        private void OnDestroy()
        {
            m_Button.onClick.RemoveListener(HandleButtonClicked);
        }

        protected virtual void HandleButtonClicked()
        {
            OnClicked.Invoke();
        }

        public void SetInteractable(bool set)
        {
            m_Button.interactable = set;
        }

        protected override void FindReferences()
        {
            base.FindReferences();
            if (m_Button == null)
            {
                m_Button = GetComponent<Button>();
            }
        }
    }
}