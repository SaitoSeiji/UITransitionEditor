using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace aojiru_UI
{
    //ボタンを押した回数で条件達成
    public class OnClickTimeBoolTerm: AbstractUIBoolTerm
    {
        [SerializeField] public int _targetClickTime;//目標回数
        [System.NonSerialized]int _nowClickTime;//現在の回数
        [SerializeField] List<GameObjectSaver> _clickTargets = new List<GameObjectSaver>();
        

        protected override bool ConcreteTerm()
        {
            return _nowClickTime >= _targetClickTime;
        }

        public override void InitAction()
        {
            base.InitAction();
            foreach (var btn in _clickTargets)
            {
                btn.GetObj().GetComponent<Button>().onClick.AddListener(() => _nowClickTime++);
            }
        }

        protected override void EnableAction()
        {
            base.EnableAction();
            _nowClickTime = 0;
        }
    }
}
