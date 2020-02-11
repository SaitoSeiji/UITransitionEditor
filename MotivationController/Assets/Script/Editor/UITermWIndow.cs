using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class UITermWIndow : DefaultWindow
{
    //windowの情報にアクセスしやすくしたい

    UITransitionTerm _transitionData;
    
    BoolTermNodeSet _boolNodeSet;

    public void OpenWindow(UITransitionTerm tranData)
    {
        _transitionData = tranData;
        _boolNodeSet = new BoolTermNodeSet(new Vector2(400, 50), new Vector2(200, 150),tranData, colorCode: 5);
        List<BoolTermNodeData> initData = _boolNodeSet.ConvertUIBoolTerm2NodeData(tranData._BoolTerms);
        DefaultWindow.AddNode(_boolNodeSet,initData);
        ShowWindow<UITermWIndow>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("add bool term"))
        {
            _transitionData.AddBoolTerm(BoolTermType.AwakeTime);
            int lastCount = _transitionData._BoolTerms.Count - 1;
            DefaultWindow.AddNode(_boolNodeSet,new BoolTermNodeData(_boolNodeSet,_transitionData,_transitionData._BoolTerms[lastCount]));
        }

        BeginWindows();
        //DrawNode(_trrigerNode,"トリガー",0);
        DefaultWindow.DrawNode(_boolNodeSet,"ブール",10);
        EndWindows();
    }

}
#endif
