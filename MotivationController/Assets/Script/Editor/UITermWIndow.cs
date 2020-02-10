using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class UITermWIndow : WindowCreater
{
    UITransitionTerm _transitionData;

    //SimpleNodeSet _trrigerNode = new SimpleNodeSet(new Vector2(50, 50), new Vector2(150, 150),false,3,colorCode:3);
    BoolTermNodeSet _boolNode = new BoolTermNodeSet(new Vector2(400, 50), new Vector2(200, 150), colorCode:5);

    int _removeNumber;

    static void ShowWindow()
    {
        EditorWindow.GetWindow<UITermWIndow>();
    }

    public void OpenWindow(UITransitionTerm tranData)
    {
        _transitionData = tranData;

        //AddNode(_trrigerNode,new );
        //_trrigerNode.AddCallback(()=>EditorGUILayout.LabelField("aa","aa"));

        AddNode(_boolNode,CreateBoolNodeList(tranData._BoolTerms));

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
            _transitionData.AddBoolTerm(BoolTermType.AwakeTime);
            AddNode(_boolNode,new BoolTermNodeData(BoolTermType.AwakeTime,_transitionData,_transitionData._BoolTerms.Count-1));
        }
        if (GUILayout.Button("remove"))
        {
            _transitionData.RemoveBoolTerm(_removeNumber);
            RemoveNode(_boolNode,_removeNumber);
        }
        _removeNumber = EditorGUILayout.IntSlider("condition", _removeNumber, 0, _transitionData._BoolTerms.Count-1);
        GUILayout.EndHorizontal();

        BeginWindows();
        //DrawNode(_trrigerNode,"トリガー",0);
        DrawNode(_boolNode,"ブール",10);
        EndWindows();
    }

     List<BoolTermNodeData> CreateBoolNodeList(List<AbstractUIBoolTerm> boolTerms)
    {
        var result = new List<BoolTermNodeData>();
        for(int i=0;i<boolTerms.Count;i++)
        {
            var data = new BoolTermNodeData(boolTerms[i].GetTermType(),_transitionData,i);
            result.Add(data);
        }
        return result;
    }
}
#endif
