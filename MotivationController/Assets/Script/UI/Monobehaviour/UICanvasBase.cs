using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIの1ページを担当するCanvasにつける 及びそれを表す
public class UICanvasBase : MonoBehaviour,IUICv_active
{
    [SerializeField] UICanvasController _uiCtrl;//自動設置したい
    [SerializeField] UITransitinData[] _condition;//条件群

    Canvas selfCanvas;//いらないかもしれない
    public Canvas SelfCanvas
    {
        get
        {
            if (selfCanvas == null)
            {
                selfCanvas = GetComponent<Canvas>();
            }
            return selfCanvas;
        }
    }

    public enum UISTATE
    {
        ACTIVE,//active=trueで入力を受け付ける
        SLEEP,//active=trueだが入力を受け付けない
        CLOSE//active=false
    }
    [SerializeField]UISTATE _nowUIState=UISTATE.CLOSE;
    public UISTATE _NowUIState { get { return _nowUIState; } }

    [SerializeField] bool canInput = false;
    public bool CanInput
    {
        get
        {
            bool result = true;
            if (_nowUIState != UISTATE.ACTIVE) result = false;
            else if (_isActiveWait.WaitNow) result = false;

            canInput = result;
            return result;
        }
    }
    #region canInputを構成する条件軍
    //入力を受け付けない待ち時間
    TimeFlag _isActiveWait = new TimeFlag();
    #endregion
    

    private void Update()
    {
        if (!CanInput) return;

        //毎フレーム条件を確認するのは非効率
        //複数の遷移条件が達成された場合の処理を想定してない
        foreach (var con in _condition)
        {
            if (con.PermitTransition())
            {
                RunTransition(con.nextUI, con.isSelfActive);
                break;
            }
        }
    }


    //実際に遷移を実行する
    void RunTransition(UICanvasBase chengeUI, bool selfActive)
    {
        if (selfActive)
        {
            _uiCtrl.AddCanvas(chengeUI);
        }
        else
        {
            _uiCtrl.CloseToNextCanvas(chengeUI);
        }
    }

    public void ChengeUIState(UISTATE state)
    {
        if (state == UISTATE.ACTIVE&&_nowUIState!=UISTATE.ACTIVE)
        {
            ActiveInitAction();
        }
        _nowUIState = state;
    }

    public void ActiveInitAction()
    {
        _isActiveWait.StartWait(0.5f);
        foreach(var con in _condition)
        {
            con.ActiveInitAction();
        }
    }
}
