namespace LSTools
{
    public abstract class AModule : CachedMonoBehaviour
    {
        protected readonly AEventBinder m_Binder = new AEventBinder();

        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            HandleInitialization();
            IsInitialized = true;
        }

        public void Uninitialize()
        {
            IsInitialized = false;
            HandleUninitialization();
            m_Binder.Unbind();
        }

        protected abstract void HandleInitialization();
        protected abstract void HandleUninitialization();
    }
}