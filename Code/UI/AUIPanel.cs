using UnityEngine;
using System.Collections;

namespace LSTools
{
    public abstract class AUIPanel : AUIBehaviour
    {
        protected readonly AEventBinder m_Binder = new AEventBinder();

        public override void Uninitialize()
        {
            m_Binder.Unbind();
            base.Uninitialize();
        }
    }
}