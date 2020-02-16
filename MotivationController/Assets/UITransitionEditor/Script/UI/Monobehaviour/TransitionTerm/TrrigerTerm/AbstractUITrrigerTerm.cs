﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    public enum TrrigerType
    {
        None,//非enable
        Onclick//クリックすると反応
    }
    public static class TrrigerTypeConstract
    {
        public static AbstractUITrrigerTerm ConstractTerm(TrrigerType type)
        {
            switch (type)
            {
                case TrrigerType.None:
                    return new NoneUITrrigerTerm();
                case TrrigerType.Onclick:
                    return new OncliclUITrrigerTerm();
                default:
                    return null;
            }
        }
    }

    //UIの遷移条件　トリガー条件を管理するクラス
    //具体定期なトリガー条件は子クラスで指定
    [System.Serializable]
    public abstract class AbstractUITrrigerTerm : AbstractTerm
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
        public Trriger SatisfyTrriger { get { return satisfyTrriger; } }//このクラスが指定する条件を満たしているか

        public override void AwakeAction()
        {
            coalTiming = SetCoalTiming();
            if (coalTiming == CoalTiming_StaisfyAction.AWAKE)
            {
                SetSatisfyAction();
            }
        }
        public override void StartAction()
        {
            if (coalTiming == CoalTiming_StaisfyAction.START)
            {
                SetSatisfyAction();
            }
        }

        public override void UpdateAction()
        {
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

        public override void TranspotMessage_uiActive()
        {
            satisfyTrriger._Trriger = false;
        }
        #endregion

        abstract protected bool SetSatisfyAction();//条件の達成を処理する関数
        abstract protected CoalTiming_StaisfyAction SetCoalTiming();//SetSatisfyActionが呼ばれるタイミングを指定
        abstract public TrrigerType GetTrrigerType();

        //現状ボタンで呼ばれている
        protected void SetSatisfyTrriger(bool flag)
        {
            satisfyTrriger._Trriger = flag;
        }

        #region editor

        public static System.Type GetTrrigerTermType(TrrigerType type)
        {
            switch (type)
            {
                case TrrigerType.Onclick:
                    return typeof(OncliclUITrrigerTerm);
                case TrrigerType.None:
                    return typeof(NoneUITrrigerTerm);
                default:
                    return null;
            }
        }
        #endregion
        
    }
}
