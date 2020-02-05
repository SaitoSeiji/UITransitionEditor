using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//とりあえず作ってみるもの
//UIパネルにつける
public class pre_UI : MonoBehaviour
{
    [SerializeField] UITransitinData[] _condition;//条件群

    //入力を受け付けない待ち時間
    TimeFlag isActiveWait=new TimeFlag();
    

    private void OnEnable()
    {
        //アクティブになった時の入力非受付時間の指定
        //時間の長さや長さの指定方法などは後で整えたい
        isActiveWait.StartWait(0.5f);
    }

    private void Update()
    {
        if (isActiveWait.WaitNow) return;

        //毎フレーム条件を確認するのは非効率
        //複数の遷移条件が達成された場合の処理を想定してない
        foreach(var con in _condition)
        {
            if (con.PermitTransition())
            {
                RunTransition(con.nextUI, con.isSelfActive);
                break;
            }
        }
    }


    //実際に遷移を実行する
    //SetActiveのところをいい感じの関数に置き換えていきたい
    void RunTransition(GameObject chengeUI, bool selfActive)
    {
        chengeUI.SetActive(true);
        this.gameObject.SetActive(selfActive);
    }
}
