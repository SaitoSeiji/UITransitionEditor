using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//指定のボタンを押すと条件を満たす
public class OncliclUITrrigerTerm : AbstractUITrrigerTerm
{
    [SerializeField] Button[] targetButtons;//指定のボタン

    protected override CoalTiming_StaisfyAction SetCoalTiming()
    {
        return CoalTiming_StaisfyAction.AWAKE;
    }

    protected override bool SetSatisfyAction()
    {
        //指定のボタンに「押されたら条件達成」を設定
        foreach(var bt in targetButtons)
        {
            bt.onClick.AddListener(() => SetSatisfyTrriger(true));
        }
        return true;
    }
}
