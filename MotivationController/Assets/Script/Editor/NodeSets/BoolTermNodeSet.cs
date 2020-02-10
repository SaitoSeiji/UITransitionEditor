using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class BoolTermNodeSet : NodeSet<BoolTermNodeData>
{
    public BoolTermNodeSet(Vector2 firstPos, Vector2 nodeSize, int colorCode = 0)
        : base(firstPos, nodeSize, colorCode)
    {
    }
    public BoolTermNodeSet(Vector2 firstPos, Vector2 nodeSize
        , bool arrangeX, int arrangeCount, int colorCode = 0)
        : base(firstPos, nodeSize, arrangeX, arrangeCount, colorCode)
    {
    }
    
}

public class BoolTermNodeData : NodeData
{
    BoolTermType _myType;
    

    UITransitionTerm _myTerms;
    int _termIndex;

    public BoolTermNodeData()
    {

    }

    public BoolTermNodeData(BoolTermType type, UITransitionTerm tranTerms,int termIndex)
    {
        _myType = type;
        _myTerms = tranTerms;
        _termIndex = termIndex;
    }

    public override void AbstractCallBack()
    {
        EditorGUI.BeginChangeCheck();
        _myType = (BoolTermType)EditorGUILayout.EnumPopup("Type", _myType);
        if (EditorGUI.EndChangeCheck())
        {
            //_myBoolTerm=SetBoolTerm(_myType);
            _myTerms.SetBoolTerm(_termIndex, _myType);
        }

        switch (_myType)
        {
            case BoolTermType.AwakeTime:
                {
                    AwakeTimeBoolTerm term = (AwakeTimeBoolTerm)GetMyBoolTerm();
                    if (term == null) break;
                    term.waitLength = EditorGUILayout.FloatField("待ち時間", term.waitLength);
                    break;
                }
            case BoolTermType.OnClickTime:
                {
                    OnClickTimeBoolTerm term = (OnClickTimeBoolTerm)GetMyBoolTerm();
                    if (term == null) break;
                    term._targetClickTime = EditorGUILayout.IntField("押す回数", term._targetClickTime);
                    term._ClickTargetHead = EditorGUILayout.ObjectField("ボタン", term._ClickTargetHead, typeof(Button), true) as Button;
                    break;
                }
        }
    }


    AbstractUIBoolTerm GetMyBoolTerm()
    {
        if (_myTerms==null||_myTerms._BoolTerms.Count <= _termIndex) return null;
        return  _myTerms._BoolTerms[_termIndex];
    }
}
