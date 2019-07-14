using System;
using UGF.Time.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Time.Editor
{
    [CustomPropertyDrawer(typeof(TimeDate))]
    internal class TimeDateDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty propertyValue = property.FindPropertyRelative("m_value");

            ulong value = (ulong)propertyValue.longValue;
            var time = new TimeDate(value);
            var date = new DateTime(time.Ticks);
            string text = date.ToString("O");

            text = EditorGUI.DelayedTextField(position, label, text);

            if (DateTime.TryParse(text, out date))
            {
                time = date;

                propertyValue.longValue = (long)time.Value;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
