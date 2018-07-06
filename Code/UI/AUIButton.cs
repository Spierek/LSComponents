using UnityEngine;
using UnityEngine.UI;

namespace LSTools
{
    public abstract class AUIButton : AUIBehaviour
    {
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

        protected abstract void HandleButtonClicked();

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