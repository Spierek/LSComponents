using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LSTools
{
    [RequireComponent(typeof(Button))]
    public class UIButton : AUIBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public readonly AEvent OnClicked = new AEvent();
        public readonly AEvent OnDown = new AEvent();
        public readonly AEvent OnUp = new AEvent();

        public bool IsHeld { get; private set; }

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

        public void OnPointerDown(PointerEventData eventData)
        {
            IsHeld = true;
            OnDown.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsHeld = false;
            OnUp.Invoke();
        }
    }
}