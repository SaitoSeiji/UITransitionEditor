﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(UICanvasBase))]
public class UICanvasBaseEditor : Editor
{
    int _conditionNumber;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        var canvas = target as UICanvasBase;
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("init"))
        {
            canvas.InitConditionParent();
        }

        if (GUILayout.Button("edit term"))
        {
            UITermWIndow _window =ScriptableObject.CreateInstance("UITermWIndow") as UITermWIndow;// new UITermWIndow();
            _window.OpenWindow(canvas._Condition[_conditionNumber]._transitionTerms);
        }
        int condLimit = canvas._ConditionCount - 1;
        _conditionNumber = EditorGUILayout.IntSlider("condition", _conditionNumber, 0, condLimit);
        
        EditorGUILayout.EndVertical();
    }
}
#endif
