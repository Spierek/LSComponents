namespace LSTools
{
    public abstract class AModule : CachedMonoBehaviour
    {
        protected readonly AEventBinder m_Binder = new AEventBinder();

        public void Initialize()
        {
            HandleInitialization();
        }

        public void Uninitialize()
        {
            HandleUninitialization();
            m_Binder.Unbind();
        }

        protected abstract void HandleInitialization();
        protected abstract void HandleUninitialization();
    }
}