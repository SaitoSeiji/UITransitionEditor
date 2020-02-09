using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class UITermWIndow : WindowCreater
{
    //NodeSet _testNode=new NodeSet(new Vector2(50,50),new Vector2(100,100));
    UITransitionTerm _transitionData;

    NodeSet _trrigerNode = new NodeSet(new Vector2(50, 50), new Vector2(150, 150),false,3);
    NodeSet _boolNode = new NodeSet(new Vector2(400, 50), new Vector2(150, 150));

    int _removeNumber;

    static void ShowWindow()
    {
        EditorWindow.GetWindow<UITermWIndow>();
    }

    public void OpenWindow(UITransitionTerm tranData)
    {
        _transitionData = tranData;
        AddNode(_trrigerNode, 1);
        AddNode(_boolNode, tranData._BoolTerms.Count);
        ShowWindow();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("add"))
        {
            _transitionData.AddBoolTerm(null);
            AddNode(_boolNode);
        }
        if (GUILayout.Button("remove"))
        {
            _transitionData.RemoveBoolTerm(_removeNumber);
            RemoveNode(_boolNode,_removeNumber);
        }
        _removeNumber = EditorGUILayout.IntSlider("condition", _removeNumber, 0, _transitionData._BoolTerms.Count-1);
        GUILayout.EndHorizontal();

        BeginWindows();
        DrawNode(_trrigerNode,"トリガー",0);
        DrawNode(_boolNode,"ブール",10);
        EndWindows();
    }
}
#endif
