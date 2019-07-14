using System;
using UGF.Time.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Time.Editor
{
    [CustomPropertyDrawer(typeof(TimeTicks))]
    internal class TimeTicksDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty propertyValue = property.FindPropertyRelative("m_value");

            long value = propertyValue.longValue;
            var ticks = new TimeTicks(value);
            TimeSpan span = ticks;
            string text = span.ToString("G");

            text = EditorGUI.DelayedTextField(position, label, text);

            if (TimeSpan.TryParse(text, out span))
            {
                ticks = span;

                propertyValue.longValue = ticks.Value;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
