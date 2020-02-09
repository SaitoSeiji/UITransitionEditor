using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(UICanvasBase))]
public class UICanvasBaseEditor : Editor
{

    int _elementNumber;
    int _termnumber;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        var canvas = target as UICanvasBase;
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("edit term"))
        {
            Debug.Log("edit "+_elementNumber);
            UITermWIndow _window = new UITermWIndow();
            _window.OpenWindow(canvas._Condition[_elementNumber]._transitionTerms[_termnumber]);
        }
        _elementNumber = EditorGUILayout.IntSlider("condition", _elementNumber, 0, GetTermCount(canvas));
        _termnumber = EditorGUILayout.IntSlider("terms", _termnumber, 0, canvas._Condition[_elementNumber]._transitionTerms.Length-1);
        EditorGUILayout.EndVertical();
    }

    int GetTermCount(UICanvasBase canvas)
    {
        return canvas._ConditionCount-1;
    }
}
#endif
