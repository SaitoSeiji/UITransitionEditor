﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIの遷移条件と遷移先を保持するクラス
//pre_UIが保持
[System.Serializable]
public class UITransitinData:IUICv_active
{
    public bool isSelfActive;//trueなら遷移後もアクティブ
    public UICanvasBase nextUI;//遷移先
    public UITransitionTerm[] _transitionTerms;//遷移条件 どれかが達成されればいい

    //遷移許可フラグ
    //許可を確認したら必ず遷移するのでTrrigerになってる
    Trriger permitTranstion = new Trriger();

    ///遷移の許可を出す
    public bool PermitTransition()
    {
        if (permitTranstion._Trriger)
        {
            return true;
        }
        else
        {
            permitTranstion._Trriger = CheckSomeMeetTerm();
            return false;
        }
    }

    public void ActiveInitAction()
    {
        foreach(var term in _transitionTerms)
        {
            term.ActiveInitAction();
        }
    }


    bool CheckSomeMeetTerm()
    {
        //1つでも条件を満たしているものがあればtrue
        foreach(var cond in _transitionTerms)
        {
            if (cond.IsMeetTerms()) return true;
        }

        return false;
    }
}
