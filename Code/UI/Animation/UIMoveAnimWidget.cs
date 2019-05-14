using UnityEngine;

namespace LSTools
{
    public class UIMoveAnimWidget : AUIAnimWidget
    {
        [Space]
        [SerializeField]
        protected RectTransform m_AnimTransform;
        [SerializeField]
        private AnimationCurve m_MovementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("Horizontal")]
        [SerializeField]
        private Vector2 m_StartPos = new Vector2(0.7f, 0.4f);
        [SerializeField]
        private Vector2 m_EndPos = new Vector2(0.9f, 0.7f);

        protected override void HandleAnimation(float ratio)
        {
            // set anchors and adjust position
            float posRatio = m_MovementCurve.Evaluate(ratio);
            Vector2 targetPos = Vector2.zero;
            targetPos.x = Mathf.Lerp(m_StartPos.x, m_EndPos.x, posRatio);
            targetPos.y = Mathf.Lerp(m_StartPos.y, m_EndPos.y, posRatio);

            m_AnimTransform.anchorMin = targetPos;
            m_AnimTransform.anchorMax = targetPos;
            m_AnimTransform.anchoredPosition = Vector2.zero;
        }
    }
}