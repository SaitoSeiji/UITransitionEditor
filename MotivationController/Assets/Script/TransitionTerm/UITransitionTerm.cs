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
        [SerializeField] protected AbstractUITrrigerTerm _trrigerTerm;
        [SerializeField] protected List<AbstractUIBoolTerm> _boolTerms = new List<AbstractUIBoolTerm>(); //bool条件　複数設定可能
        
        #region termの登録
        //factory系の何かが使える？
        public void SetTrriger(AbstractUITrrigerTerm term)
        {
            _trrigerTerm = term;
        }

        public void AddBool(AbstractUIBoolTerm term)
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

        //Termが持っているinitActionが再帰的に呼ばれるようにしたい
        public override void InitAction()
        {
            base.InitAction();
            TermMonoActor.Instance.RegisterTerm(_trrigerTerm);
            foreach(var data in _boolTerms)
            {
                TermMonoActor.Instance.RegisterTerm(data);
            }
        }
    }
}
