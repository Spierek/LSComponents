namespace LSTools
{
    public abstract class AModule : CachedMonoBehaviour
    {
        public abstract void Initialize();
        public abstract void Uninitialize();
    }
}