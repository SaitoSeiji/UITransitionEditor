using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    //UIの遷移条件を管理する
    [System.Serializable]
    public class UITransitionTerm : AbstractTransitionTerm
    {
        //bool条件をすべて満たした状態で　トリガー条件を達成すると遷移可能
        [SerializeField] public AbstractUITrrigerTerm _trrigerTerm { get; protected set; }

        [SerializeField] public List<AbstractUIBoolTerm> _boolTerms=new List<AbstractUIBoolTerm>();
        

        #region termの登録
        //factory系の何かが使える？
        public void SetTrriger(AbstractUITrrigerTerm term)
        {
            _trrigerTerm = term;
        }

        public void AddBool(AbstractUIBoolTerm term)
        {
            if (_boolTerms == null) _boolTerms = new List<AbstractUIBoolTerm>();
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


        #region あんまり意味のない継承
        //再帰的に呼ばれるようにしたい
        public override void InitAction()
        {
            base.InitAction();
            var regist = MonoActionRegister.Instance;
            regist.RegisterAwakeAction(()=>_trrigerTerm.InitAction());
            regist.RegisterUpdateAction(() => _trrigerTerm.UpdateAction());
            foreach(var data in _boolTerms)
            {
                regist.RegisterAwakeAction(() => data.InitAction());
                regist.RegisterUpdateAction(() => data.UpdateAction());
            }
        }

        protected override void EnableAction()
        {
            base.EnableAction();
            _trrigerTerm.SetEnable(true);
            foreach(var term in _boolTerms)
            {
                term.SetEnable(true);
            }
        }

        protected override void DisableAction()
        {
            base.DisableAction();
            _trrigerTerm.SetEnable(false);
            foreach (var term in _boolTerms)
            {
                term.SetEnable(false);
            }
        }
        #endregion
    }
}
