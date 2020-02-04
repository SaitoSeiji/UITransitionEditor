using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickTimeBoolTerm : AbstractUIBoolTerm
{
    [SerializeField] int _targetClickTime;
    [SerializeField] int _nowClickTime;
    [SerializeField] Button[] _clickTargets;

    protected override bool ConcreteTerm()
    {
        return _nowClickTime >= _targetClickTime;
    }

    public void CountUp()
    {
        _nowClickTime++;
    }

    protected override void InitAction()
    {
        base.InitAction();
        foreach(var btn in _clickTargets)
        {
            btn.onClick.AddListener(() => CountUp());
        }
    }

    protected override void EnableAction()
    {
        base.EnableAction();
        _nowClickTime = 0;
    }
}
