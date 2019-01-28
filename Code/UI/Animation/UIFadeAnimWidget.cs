using UnityEngine;

namespace LSTools
{
    public class UIFadeAnimWidget : AUIAnimWidget
    {
        [Space]
        [SerializeField]
        protected CanvasGroup m_AnimGroup;
        [SerializeField]
        private AnimationCurve m_AlphaCurve = AnimationCurve.Linear(0, 1, 1, 1);

        protected override void HandleShow()
        {
            base.HandleShow();
            m_AnimGroup.alpha = 0f;
        }

        protected override void HandleAnimation(float ratio)
        {
            // set alpha
            float alphaRatio = m_AlphaCurve.Evaluate(ratio);
            m_AnimGroup.alpha = alphaRatio;
        }
    }
}