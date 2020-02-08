using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ボタンを押した回数で条件達成
public class OnClickTimeBoolTerm : AbstractUIBoolTerm
{
    [SerializeField] int _targetClickTime;//目標回数
    [SerializeField] int _nowClickTime;//現在の回数
    [SerializeField] Button[] _clickTargets;

    protected override bool ConcreteTerm()
    {
        return _nowClickTime >= _targetClickTime;
    }

    protected override void InitAction()
    {
        base.InitAction();
        //ボタンに条件を設定
        foreach(var btn in _clickTargets)
        {
            btn.onClick.AddListener(() => _nowClickTime++);
        }
    }

    public override void TranspotMessage_uiActive()
    {
        _nowClickTime = 0;
    }
}
