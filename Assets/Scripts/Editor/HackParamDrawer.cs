using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HackParam))]
public class HackParamDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        int storedIndent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        SerializedProperty type = property.FindPropertyRelative("type");

        Rect nameRect = new Rect(position.x - 50, position.y, 100, position.height - 2);
        Rect typeRect = new Rect(position.x + 55, position.y, 75, position.height);
        Rect valueRect = new Rect(position.x + 135, position.y, position.width - 140, position.height - 2);

        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);
        EditorGUI.PropertyField(typeRect, type, GUIContent.none);

        switch((HackParamTypes)type.enumValueIndex)
        {
            case HackParamTypes.String:

                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("valueAsString"), GUIContent.none);

                break;

            case HackParamTypes.Int:

                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("valueAsInt"), GUIContent.none);

                break;

            case HackParamTypes.Float:

                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("valueAsFloat"), GUIContent.none);

                break;

            case HackParamTypes.Vector3:

                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("valueAsVector3"), GUIContent.none);

                break;
        }

        EditorGUI.indentLevel = storedIndent;

        EditorGUI.EndProperty();
    }
}
