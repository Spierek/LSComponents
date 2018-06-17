using UnityEditor;
using UnityEngine;

namespace LSTools
{
    [CustomEditor(typeof(AFSMModule), true)]
    public class AFSMModuleEditor : Editor
    {
        private AFSMModule m_Module;

        public override bool RequiresConstantRepaint()
        {
            return true;
        }

        private void OnEnable()
        {
            m_Module = target as AFSMModule;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawFSM();
        }

        private void DrawFSM()
        {
            if (!Application.isPlaying || m_Module == null || m_Module.FSM == null)
            {
                EditorGUILayout.HelpBox("State Machine can only be previewed in Play Mode.", MessageType.Info);
            }
            else
            {
                EditorGUILayout.LabelField("State Machine:", EditorStyles.boldLabel);
                DrawStateMachine(m_Module.FSM);
            }
        }

        private void DrawStateMachine(FiniteStateMachine stateMachine)
        {
            foreach (AState state in stateMachine.GetStates().Values)
            {
                bool isCurrent = (stateMachine.CurrentStateId == state.Id);
                DrawState(state, isCurrent);
            }
        }

        private void DrawState(AState state, bool isCurrent)
        {
            // draw state name
            string name = (isCurrent ? "▶" : "") + state.ToString();
            EditorGUILayout.LabelField(name);

            // draw substates
            if (state.FSM.HasStates())
            {
                EditorGUI.indentLevel++;
                DrawStateMachine(state.FSM);
                EditorGUI.indentLevel--;
            }
        }
    }

}