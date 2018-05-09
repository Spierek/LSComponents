namespace LSTools
{
    public abstract class APoolable : CachedMonoBehaviour
    {
        private AComponentPool m_ParentPool;

        public void Create(AComponentPool pool)
        {
            m_ParentPool = pool;
            HandleCreation();
        }
        protected virtual void HandleCreation() { }

        public void Spawn()
        {
            HandleSpawn();
        }
        protected virtual void HandleSpawn() { }

        public void PoolDespawn()
        {
            HandleDespawn();
        }

        public void Despawn()
        {
            HandleDespawn();
            m_ParentPool.Despawn(this);
        }
        protected virtual void HandleDespawn() { }
    }
}