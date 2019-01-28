using UnityEngine;

namespace LSTools
{
    public abstract class AUIAnimWidget : AUIBehaviour
    {
        [SerializeField, Range(0.5f, 7f)]
        protected float m_AnimationDuration = 2f;

        protected float m_AnimationStartTime = 0f;

        protected override void Update()
        {
            if (IsVisible)
            {
                UpdateAnimation();
            }
        }

        protected override void HandleShow()
        {
            base.HandleShow();
            m_AnimationStartTime = Time.time;
        }

        private void UpdateAnimation()
        {
            float totalTime = Time.time - m_AnimationStartTime;
            float timer = totalTime % m_AnimationDuration;
            float ratio = timer / m_AnimationDuration;

            HandleAnimation(ratio);
        }

        protected abstract void HandleAnimation(float ratio);
    }
}