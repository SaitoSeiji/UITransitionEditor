using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIの遷移条件の条件指定を行う
//トリガー条件の具体的な条件については子クラスで指定
public class Pre_UITermsDesignation : MonoBehaviour
{
    Pre_uiTransitionCondition myTransition;

    //bool条件をすべて満たした状態で　トリガー条件を達成すると遷移可能
    Trriger _trrigerTerm = new Trriger();//トリガー条件　一つの条件に一つのみ　トリガー条件が満たされる条件を子クラスで指定している　が、微妙
    [SerializeField] bool _onlyBoolTerm;//トリガー条件を使用しない
    [SerializeField]AbstractUIBoolTerm[] _boolTerms; //bool条件　複数設定可能 自動読み込みしてほしい

    public void SetMyTransition(Pre_uiTransitionCondition cond)
    {
        myTransition = cond;
    }

    private void Awake()
    {
        AwakeActoin();
    }
    protected virtual void AwakeActoin()
    {

    }

    private void Update()
    {
        if (IsMeetTerms())
        {
            //遷移を許可
            myTransition.PermitTransition = true;
        }
    }

    //遷移の条件を満たしている
    bool IsMeetTerms()
    {
        if (_trrigerTerm._Trriger||_onlyBoolTerm)
        {
            //トリガー条件を達成した状態で　bool条件をすべて満たすと遷移可能
            foreach (var term in _boolTerms)
            {
                if (!term._IsSatisfy) return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    //現状ボタンで呼ばれている
    public void SetTrrigerTerm(bool flag)
    {
        _trrigerTerm._Trriger = flag;
    }
}
