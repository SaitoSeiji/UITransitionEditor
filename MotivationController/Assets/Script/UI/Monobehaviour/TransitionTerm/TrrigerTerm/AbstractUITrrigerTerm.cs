using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIの遷移条件　トリガー条件を管理するクラス
//具体定期なトリガー条件は子クラスで指定
public abstract class AbstractUITrrigerTerm : MonoBehaviour
{
    //SetSatisfyActionが呼ばれるタイミングを指定
    public enum CoalTiming_StaisfyAction
    {
        AWAKE,//ボタンなどで呼ぶなら
        START,
        UPDATE//bool条件として扱うなら
    }
    CoalTiming_StaisfyAction coalTiming;

    Trriger satisfyTrriger = new Trriger();
    public Trriger SatisfyTrriger { get { return satisfyTrriger; }}//このクラスが指定する条件を満たしているか

    private void Awake()
    {
        coalTiming = SetCoalTiming();
        if (coalTiming == CoalTiming_StaisfyAction.AWAKE)
        {
            SetSatisfyAction();
        }
    }

    private void Start()
    {
        if (coalTiming == CoalTiming_StaisfyAction.START)
        {
            SetSatisfyAction();
        }
    }

    private void Update()
    {
        if (coalTiming == CoalTiming_StaisfyAction.UPDATE)
        {
            if (SetSatisfyAction())
            {
                SetSatisfyTrriger(true);
            }
        }
    }

    abstract protected bool SetSatisfyAction();//条件の達成を処理する関数
    abstract protected CoalTiming_StaisfyAction SetCoalTiming();//SetSatisfyActionが呼ばれるタイミングを指定

    //現状ボタンで呼ばれている
    protected void SetSatisfyTrriger(bool flag)
    {
        satisfyTrriger._Trriger = flag;
    }
}
