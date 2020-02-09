﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIの遷移条件を管理する
[System.Serializable]
public class UITransitionTerm
{
    //bool条件をすべて満たした状態で　トリガー条件を達成すると遷移可能
    [SerializeField]protected AbstractUITrrigerTerm _trrigerTerm;
    public AbstractUITrrigerTerm _TrrigerTerm { get { return _trrigerTerm; } }
    [SerializeField]protected List<AbstractUIBoolTerm> _boolTerms=new List<AbstractUIBoolTerm>(); //bool条件　複数設定可能
    public List<AbstractUIBoolTerm> _BoolTerms { get { return _boolTerms; } }

    //遷移の条件を満たしている
    public bool IsMeetTerms()
    {
        if (_trrigerTerm==null||_trrigerTerm.SatisfyTrriger._Trriger)
        {
            //トリガー条件を達成した状態で　bool条件をすべて満たすと遷移可能
            foreach (var term in _boolTerms)
            {
                //1つでも満たしていなければfalse
                if (!term._IsSatisfy) return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }


    #region Editor

    public void AddBoolTerm(AbstractUIBoolTerm term)
    {
        _boolTerms.Add(term);
    }

    public void RemoveBoolTerm(int index)
    {
        _boolTerms.RemoveAt(index);
    }
    #endregion
}
