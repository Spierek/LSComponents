using System.Collections.Generic;

namespace LSTools
{
    public abstract class AUIManager : AModule
    {
        private List<AUIPanel> m_InitializedPanels = new List<AUIPanel>();

        protected override void HandleUninitialization()
        {
            ClearPanels();
        }

        protected void AddPanel(AUIPanel panel)
        {
            if (panel != null)
            {
                panel.Initialize();
                m_InitializedPanels.Add(panel);
            }
        }

        protected void RemovePanel(AUIPanel panel, bool isClearing = false)
        {
            if (panel != null)
            {
                panel.Uninitialize();
                if (!isClearing)
                {
                    m_InitializedPanels.Remove(panel);
                }
            }
        }

        // removes all panels in reverse order
        protected void ClearPanels()
        {
            for (int i = m_InitializedPanels.Count - 1; i >= 0; --i)
            {
                AUIPanel panel = m_InitializedPanels[i];
                RemovePanel(panel, true);
            }

            m_InitializedPanels.Clear();
        }
    }
}
