﻿using RustedGames.Serializable;
using UnityEditor;
using UnityEngine;

namespace RustedGames.Editors
{
    [CustomPropertyDrawer(typeof(SerializableVector2))]
    public class SerializableVector2Drawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            SerializedProperty x = property.FindPropertyRelative("x");
            SerializedProperty y = property.FindPropertyRelative("y");

            EditorGUI.BeginChangeCheck();
            Vector2 c = new Vector2(x.floatValue, y.floatValue);
            c = EditorGUI.Vector2Field(position, label, c);
            x.floatValue = c.x;
            y.floatValue = c.y;

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.EndProperty();
        }
    }
}
