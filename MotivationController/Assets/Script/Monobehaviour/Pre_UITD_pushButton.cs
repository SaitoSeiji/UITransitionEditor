using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Pre_UITermsDesignationのコンクリート
//指定したボタンを押したら条件を満たす
public class Pre_UITD_pushButton : Pre_UITermsDesignation
{
    [SerializeField] Button[] _termsButtons;//どれかが押されたら条件達成
    

    protected override void AwakeActoin()
    {
        base.AwakeActoin();

        //対象のボタンが押されたら条件達成　を登録
        foreach(var button in _termsButtons)
        {
            button.onClick.AddListener(() => SetTrrigerTerm(true));
        }
    }
}
