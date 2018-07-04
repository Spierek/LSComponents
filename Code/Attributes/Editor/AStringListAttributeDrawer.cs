using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace LSTools
{
    [CustomPropertyDrawer(typeof(AStringListAttribute), true)]
    public class AStringListAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            AStringListAttribute stringInList = attribute as AStringListAttribute;
            string[] list = stringInList.List;
            if (property.propertyType == SerializedPropertyType.String)
            {
                property.stringValue = DrawStringDrawer(position, property, list);
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                property.intValue = EditorGUI.Popup(position, property.displayName, property.intValue, list);
            }
            else
            {
                base.OnGUI(position, property, label);
            }
        }

        public static string DrawStringDrawer(Rect position, SerializedProperty property, string[] list)
        {
            int index = Mathf.Max(0, Array.IndexOf(list, property.stringValue));
            index = EditorGUI.Popup(position, property.displayName, index, list);

            return list[index];
        }
    }
}