using UnityEngine;

namespace LSTools
{
    public class EventCollider2D : MonoBehaviour
    {
        [SerializeField]
        private Collider2D m_Collider;
        public Collider2D Collider { get { return m_Collider; } }

        public AEvent<Collision2D> OnColliderIntersection = new AEvent<Collision2D>();   // entered + stayed
        public AEvent<Collision2D> OnColliderEntered = new AEvent<Collision2D>();
        public AEvent<Collision2D> OnColliderStayed = new AEvent<Collision2D>();
        public AEvent<Collision2D> OnColliderExited = new AEvent<Collision2D>();

        public AEvent<Collider2D> OnTriggerIntersection = new AEvent<Collider2D>();      // entered + stayed
        public AEvent<Collider2D> OnTriggerEntered = new AEvent<Collider2D>();
        public AEvent<Collider2D> OnTriggerStayed = new AEvent<Collider2D>();
        public AEvent<Collider2D> OnTriggerExited = new AEvent<Collider2D>();

        private void Awake()
        {
            GetReferences();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnColliderIntersection.Invoke(collision);
            OnColliderEntered.Invoke(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            OnColliderIntersection.Invoke(collision);
            OnColliderStayed.Invoke(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            OnColliderExited.Invoke(collision);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerIntersection.Invoke(other);
            OnTriggerEntered.Invoke(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerIntersection.Invoke(other);
            OnTriggerStayed.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnTriggerExited.Invoke(other);
        }

        private void OnValidate()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            if (m_Collider == null)
            {
                m_Collider = GetComponent<Collider2D>();
            }
        }
    }

}