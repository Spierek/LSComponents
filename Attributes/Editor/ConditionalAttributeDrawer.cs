using UnityEngine;
using UnityEditor;

namespace LSTools
{
    [CustomPropertyDrawer(typeof(ConditionalAttribute))]
    public class ConditionalPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ConditionalAttribute condAttribute = attribute as ConditionalAttribute;
            if (condAttribute != null)
            {
                bool wasEnabled = GUI.enabled;
                bool enabled = GetConditionalAttributeResult(condAttribute, property) && wasEnabled;
                GUI.enabled = enabled;
                if (!condAttribute.HideInInspector || enabled)
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
                GUI.enabled = wasEnabled;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ConditionalAttribute condAttribute = attribute as ConditionalAttribute;
            bool enabled = GetConditionalAttributeResult(condAttribute, property);
            if (!condAttribute.HideInInspector || enabled)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
            else
            {
                return -EditorGUIUtility.standardVerticalSpacing;
            }
        }

        private bool GetConditionalAttributeResult(ConditionalAttribute condAttribute, SerializedProperty property)
        {
            if (condAttribute == null)
            {
                return true;
            }

            if (condAttribute.VariableName != string.Empty)
            {
                return GetConditionalResult(condAttribute.VariableName, condAttribute.IsTrue, property);
            }
            else
            {
                return GetConditionalPairResult(condAttribute.Pair, property);
            }
        }

        private bool GetConditionalPairResult(ConditionalPair pair, SerializedProperty property)
        {
            string propertyPath = property.propertyPath;
            bool resultA = GetConditionalResult(pair.VariableA, pair.ATrue, property);
            bool resultB = GetConditionalResult(pair.VariableB, pair.BTrue, property);

            switch (pair.Operator)
            {
                case ELogicOperator.AND: return resultA && resultB;
                case ELogicOperator.OR: return resultA || resultB;
            }

            return true;
        }

        private bool GetConditionalResult(string variableName, bool isTrue, SerializedProperty property)
        {
            string propertyPath = property.propertyPath;
            string conditionPath = propertyPath.Replace(property.name, variableName);
            SerializedProperty conditionProperty = property.serializedObject.FindProperty(conditionPath);
            bool conditionValue = true;
            if (conditionProperty != null)
            {
                conditionValue = (conditionProperty.boolValue == isTrue);
            }

            return conditionValue;
        }
    }
}