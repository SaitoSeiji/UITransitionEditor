using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(UICanvasBase))]
public class UICanvasBaseEditor : Editor
{
    int _conditionNumber;
    int _termNumber;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        var canvas = target as UICanvasBase;
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("edit term"))
        {
            UITermWIndow _window = new UITermWIndow();
            _window.OpenWindow(canvas._Condition[_conditionNumber]._transitionTerms[_termNumber]);
        }
        int condLimit = canvas._ConditionCount - 1;
        _conditionNumber = EditorGUILayout.IntSlider("condition", _conditionNumber, 0, condLimit);

        int termLimit = canvas._Condition[_conditionNumber]._transitionTerms.Length - 1;
        _termNumber = EditorGUILayout.IntSlider("terms", _termNumber, 0, termLimit);
        EditorGUILayout.EndVertical();
    }
}
#endif
