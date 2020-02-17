using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    public enum TrrigerType
    {
        None,//非enable
        Onclick//クリックすると反応
    }


    //UIの遷移条件　トリガー条件を管理するクラス
    //具体定期なトリガー条件は子クラスで指定
    public abstract class AbstractUITrrigerTerm<T>:AbstractTransitionTerm<TransitionLine<T>,T>
    {
        //SetSatisfyActionが呼ばれるタイミングを指定
        public enum CoalTiming_StaisfyAction
        {
            AWAKE,//ボタンなどで呼ぶなら
            START,
            UPDATE//bool条件として扱うなら
        }

        [System.NonSerialized]CoalTiming_StaisfyAction coalTiming;

        [System.NonSerialized] Trriger satisfyTrriger = new Trriger();//このクラスが指定する条件を満たしているか

        public override bool MeetTerm()
        {
            return satisfyTrriger._Trriger;
        }

        public override void AwakeAction()
        {
            base.AwakeAction();
            coalTiming = SetCoalTiming();
            if (coalTiming == CoalTiming_StaisfyAction.AWAKE)
            {
                SetSatisfyAction();
            }
        }
        public override void StartAction()
        {
            base.StartAction();
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
        #region interfaceの実装
        //対象のUICanvasのstateがActiveになったら呼ばれる初期化関数
        //public void TranspotMessage_uiActive()
        //{
        //    satisfyTrriger._Trriger = false;
        //}
        //public void SetTransportParent_privete()
        //{
        //    var parent = MessageTransporter.FindParentTransporter(transform);
        //    if (parent != null) parent.SetMessageTarget(gameObject);
        //}
        #endregion

        abstract protected bool SetSatisfyAction();//条件の達成を処理する関数
        abstract protected CoalTiming_StaisfyAction SetCoalTiming();//SetSatisfyActionが呼ばれるタイミングを指定

        //現状ボタンで呼ばれている
        protected void SetSatisfyTrriger(bool flag)
        {
            satisfyTrriger._Trriger = flag;
        }
    }
}
