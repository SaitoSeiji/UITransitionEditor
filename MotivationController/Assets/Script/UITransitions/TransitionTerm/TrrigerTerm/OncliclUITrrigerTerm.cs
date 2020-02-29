using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace aojiru_UI
{
    //指定のボタンを押すと条件を満たす
    public class OncliclUITrrigerTerm : AbstractUITrrigerTerm
    {
        [SerializeField] List<GameObjectSaver> _targetButtons=new List<GameObjectSaver>();

        protected override CoalTiming_StaisfyAction SetCoalTiming()
        {
            return CoalTiming_StaisfyAction.START;
        }

        protected override bool SetSatisfyAction()
        {
            //指定のボタンに「押されたら条件達成」を設定
            foreach (var bt in _targetButtons)
            {
                bt.GetObj().GetComponent<Button>().onClick.AddListener(() => SetSatisfyTrriger(true));
            }
            return true;
        }

        public void AddButton(GameObject button)
        {
            if (_targetButtons == null) _targetButtons = new List<GameObjectSaver>();
            var holder = InstanceIdHolder.AddIdHolder(button);
            _targetButtons.Add(new GameObjectSaver(holder));
            
        }


    }
}
