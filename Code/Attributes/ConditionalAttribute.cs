using UnityEngine;
using System;

// inspired by http://www.brechtos.com/hiding-or-disabling-inspector-properties-using-propertydrawers-within-unity-5/
namespace LSTools
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
        AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class ConditionalAttribute : PropertyAttribute
    {
        public string VariableName = "";
        public bool IsTrue = true;
        public bool HideInInspector = true;
        public ConditionalPair Pair;

        public ConditionalAttribute(string variableName)
        {
            VariableName = variableName;
            IsTrue = true;
            HideInInspector = true;
        }

        public ConditionalAttribute(string variableName, bool isTrue, bool hideInInspector)
        {
            VariableName = variableName;
            IsTrue = isTrue;
            HideInInspector = hideInInspector;
        }

        public ConditionalAttribute(string variableNameA, string variableNameB)
        {
            Pair = new ConditionalPair(variableNameA, variableNameB);
            HideInInspector = true;
        }

        public ConditionalAttribute(string variableNameA, string variableNameB, ELogicOperator op)
        {
            Pair = new ConditionalPair(variableNameA, variableNameB, op);
            HideInInspector = true;
        }
    }
}