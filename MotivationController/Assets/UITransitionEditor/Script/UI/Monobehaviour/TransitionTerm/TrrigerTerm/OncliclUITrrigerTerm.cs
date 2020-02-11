using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace aojiru_UI
{
    //指定のボタンを押すと条件を満たす
    public class OncliclUITrrigerTerm : AbstractUITrrigerTerm
    {
        [SerializeField] Button[] targetButtons = new Button[1];//指定のボタン
        public Button _HeadButton { get { return targetButtons[0]; } set { targetButtons[0] = value; } }

        protected override CoalTiming_StaisfyAction SetCoalTiming()
        {
            return CoalTiming_StaisfyAction.AWAKE;
        }

        protected override bool SetSatisfyAction()
        {
            //指定のボタンに「押されたら条件達成」を設定
            foreach (var bt in targetButtons)
            {
                bt.onClick.AddListener(() => SetSatisfyTrriger(true));
            }
            return true;
        }

        public override TrrigerType GetTrrigerType()
        {
            return TrrigerType.Onclick;
        }
    }
}
