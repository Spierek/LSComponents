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
        private Vector2 m_HorizontalStartPos = new Vector2(0.7f, 0.4f);
        [SerializeField]
        private Vector2 m_HorizontalEndPos = new Vector2(0.9f, 0.7f);

        [Header("Vertical")]
        [SerializeField]
        private Vector2 m_VerticalStartPos = new Vector2(0.7f, 0.4f);
        [SerializeField]
        private Vector2 m_VerticalEndPos = new Vector2(0.9f, 0.7f);

        protected override void HandleAnimation(float ratio)
        {
            // set anchors and adjust position
            float posRatio = m_MovementCurve.Evaluate(ratio);
            Vector2 targetPos = Vector2.zero;
            if (GolfManager.Instance.IsHorizontal)
            {
                targetPos.x = Mathf.Lerp(m_HorizontalStartPos.x, m_HorizontalEndPos.x, posRatio);
                targetPos.y = Mathf.Lerp(m_HorizontalStartPos.y, m_HorizontalEndPos.y, posRatio);
            }
            else
            {
                targetPos.x = Mathf.Lerp(m_VerticalStartPos.x, m_VerticalEndPos.x, posRatio);
                targetPos.y = Mathf.Lerp(m_VerticalStartPos.y, m_VerticalEndPos.y, posRatio);
            }

            m_AnimTransform.anchorMin = targetPos;
            m_AnimTransform.anchorMax = targetPos;
            m_AnimTransform.anchoredPosition = Vector2.zero;
        }
    }
}