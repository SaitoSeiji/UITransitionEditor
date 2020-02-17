using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    //UIの遷移条件を管理する
    [System.Serializable]
    public class UITransitionTerm : AbstractTransitionTerm<UICanvasTransition, UICanvasBase>
    {
        //bool条件をすべて満たした状態で　トリガー条件を達成すると遷移可能
        [SerializeField] bool _selfActive;//遷移時に自分をactiveのままにするかどうか
        public bool _SelfActive { get { return _selfActive; } }
        [SerializeField] protected AbstractUITrrigerTerm<UICanvasBase> _trrigerTerm;
        [SerializeField] protected List<AbstractUIBoolTerm<UICanvasBase>> _boolTerms = new List<AbstractUIBoolTerm<UICanvasBase>>(); //bool条件　複数設定可能

        public UITransitionTerm(bool selfActive, UICanvasBase from, UICanvasBase to) : base(from,to)
        {
            _selfActive = selfActive;
        }
        #region termの登録
        //factory系の何かが使える？
        public void SetTrriger(AbstractUITrrigerTerm<UICanvasBase> term)
        {
            _trrigerTerm = term;
        }

        public void AddBool(AbstractUIBoolTerm<UICanvasBase> term)
        {
            _boolTerms.Add(term);
        }
        #endregion
        public override bool MeetTerm()
        {
            if (_trrigerTerm == null || _trrigerTerm.MeetTerm())
            {
                //トリガー条件を達成した状態で　bool条件をすべて満たすと遷移可能
                foreach (var term in _boolTerms)
                {
                    //1つでも満たしていなければfalse
                    if (!term.MeetTerm()) return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        
        #region monoBehaviour
        public override void AwakeAction()
        {
            base.AwakeAction();
            _trrigerTerm.AwakeAction();
            foreach(var term in _boolTerms)
            {
                term.AwakeAction();
            }
        }

        public override void StartAction()
        {
            base.StartAction();
            _trrigerTerm.StartAction();
            foreach (var term in _boolTerms)
            {
                term.StartAction();
            }
        }

        public override void UpdateAction()
        {
            base.UpdateAction();
            _trrigerTerm.UpdateAction();
            foreach (var term in _boolTerms)
            {
                term.UpdateAction();
            }
        }
        #endregion
    }
}
