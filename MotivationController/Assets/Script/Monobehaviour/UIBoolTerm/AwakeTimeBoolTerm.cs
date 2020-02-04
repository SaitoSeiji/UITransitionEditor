using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アクティブになってからの経過時間が条件
public class AwakeTimeBoolTerm : AbstractUIBoolTerm
{
    [SerializeField] float waitLength;

    TimeFlag flag;

    private void OnEnable()
    {
        flag = new TimeFlag();
        flag.StartWait(waitLength);
    }

    protected override bool ConcreteTerm()
    {
        return !flag.WaitNow;
    }
}
