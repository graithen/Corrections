using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterTest))]
public class CharacterTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CharacterTest temp = (CharacterTest)target;

        if (GUILayout.Button("Next Character"))
        {
            temp.NextCharacter();
        }
        if (GUILayout.Button("Prev Character"))
        {
            temp.PreviousCharacter();
        }
    }
}
