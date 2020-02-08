﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アクティブになってからの経過時間が条件
//指定時間を待たずに条件を満たすことがある
public class AwakeTimeBoolTerm : AbstractUIBoolTerm
{
    [SerializeField] float waitLength;

    TimeFlag flag;

    public override void TranspotMessage_uiActive()
    {
        flag = new TimeFlag();
        flag.StartWait(waitLength);
    }

    protected override bool ConcreteTerm()
    {
        return !flag.WaitNow;
    }
}
