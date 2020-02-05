using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIの遷移条件を管理する
public class UITransitionTerm : MonoBehaviour
{
    [SerializeField] Transform termDataTran;//初期化用のtransform　_trrigerTermと_boolTermをつけておく

    //bool条件をすべて満たした状態で　トリガー条件を達成すると遷移可能
    [SerializeField]AbstractUITrrigerTerm _trrigerTerm;
    [SerializeField]AbstractUIBoolTerm[] _boolTerms; //bool条件　複数設定可能

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
    //初期化用======================================

    //termを初期化
    [ContextMenu("setTerm")]
    void SetTerm()
    {
        _trrigerTerm = termDataTran.GetComponent<AbstractUITrrigerTerm>();
        _boolTerms = termDataTran.GetComponents<AbstractUIBoolTerm>();
    }
}
