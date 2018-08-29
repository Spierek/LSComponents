namespace LSTools
{
    public abstract class AModule : CachedMonoBehaviour
    {
        protected readonly AEventBinder m_Binder = new AEventBinder();

        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            if (!IsInitialized)
            {
                HandleInitialization();
                IsInitialized = true;
            }
        }

        public void Uninitialize()
        {
            if (IsInitialized)
            {
                HandleUninitialization();
                IsInitialized = false;
            }
            m_Binder.Unbind();
        }

        protected abstract void HandleInitialization();
        protected abstract void HandleUninitialization();
    }
}