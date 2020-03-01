using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    //UIの遷移条件　トリガー条件を管理するクラス
    //具体定期なトリガー条件は子クラスで指定
    public abstract class AbstractUITrrigerTerm:AbstractTransitionTerm
    {
        //SetSatisfyActionが呼ばれるタイミングを指定
        public enum CoalTiming_StaisfyAction
        {
            START,
            UPDATE//bool条件として扱うなら
        }

        [System.NonSerialized]CoalTiming_StaisfyAction coalTiming;

        [System.NonSerialized] Trriger satisfyTrriger = new Trriger();//このクラスが指定する条件を満たしているか

        public override bool MeetTerm()
        {
            return satisfyTrriger._Trriger;
        }
        
        public override void InitAction()
        {
            base.InitAction();
            coalTiming = SetCoalTiming();
            if (coalTiming == CoalTiming_StaisfyAction.START)
            {
                SetSatisfyAction();
            }
        }
        public override void UpdateAction()
        {
            base.UpdateAction();
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

        //activeになったら初期化
        protected override void EnableAction()
        {
            base.EnableAction();
            satisfyTrriger._Trriger = false;
        }
    }
}
