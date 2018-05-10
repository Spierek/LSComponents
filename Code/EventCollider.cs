using UnityEngine;

namespace LSTools
{
    public class EventCollider : MonoBehaviour
    {
        [SerializeField]
        private Collider m_Collider;
        public Collider Collider { get { return m_Collider; } }

        public AEvent<Collision> OnColliderIntersection = new AEvent<Collision>();   // entered + stayed
        public AEvent<Collision> OnColliderEntered = new AEvent<Collision>();
        public AEvent<Collision> OnColliderStayed = new AEvent<Collision>();
        public AEvent<Collision> OnColliderExited = new AEvent<Collision>();

        public AEvent<Collider> OnTriggerIntersection = new AEvent<Collider>();      // entered + stayed
        public AEvent<Collider> OnTriggerEntered = new AEvent<Collider>();
        public AEvent<Collider> OnTriggerStayed = new AEvent<Collider>();
        public AEvent<Collider> OnTriggerExited = new AEvent<Collider>();

        private void Awake()
        {
            GetReferences();
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnColliderIntersection.Invoke(collision);
            OnColliderEntered.Invoke(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            OnColliderIntersection.Invoke(collision);
            OnColliderStayed.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            OnColliderExited.Invoke(collision);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerIntersection.Invoke(other);
            OnTriggerEntered.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerIntersection.Invoke(other);
            OnTriggerStayed.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
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
                m_Collider = GetComponent<Collider>();
            }
        }
    }

}