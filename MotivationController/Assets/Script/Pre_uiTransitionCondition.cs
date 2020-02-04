using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIの遷移条件と遷移先を保持するクラス
//名前がわかりにくい
//無駄に複雑化している要因な気がする
//複数条件に対応できてない
[System.Serializable]
public class Pre_uiTransitionCondition
{
    public bool isSelfActive;//trueなら遷移後もアクティブ
    public GameObject nextUI;//遷移先
    public Pre_UITermsDesignation[] transitionConditions;//遷移条件 どれかが達成されればいい

    //遷移許可フラグ
    //許可を確認したら必ず遷移するのでTrrigerになってる
    //transitionConditionで許可される
    Trriger permitTranstion = new Trriger();
    public bool PermitTransition
    {
        get
        {
            return permitTranstion._Trriger;
        }
        set
        {
            permitTranstion._Trriger = value;
        }
    }

    public void InitAction()
    {
        //美しくない
        foreach(var tran in transitionConditions)
        {
            tran.SetMyTransition(this);
        }
    }
}
