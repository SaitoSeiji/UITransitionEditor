﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIの1ページを担当するCanvasにつける 及びそれを表す
public class UICanvasBase : MessageTransporter
{
    UICanvasController _uiCtrl { get { return UICanvasController.Instance; } }
    [SerializeField] List<UITransitinData> _condition;//条件群
    public List<UITransitinData> _Condition { get { return _condition; } }
    public int _ConditionCount { get { return _condition.Count; } }

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
    UISTATE _NowUIState { get { return _nowUIState; } }

    [SerializeField] bool canInput = false;
    bool CanInput
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

    public void ChengeUIState(UISTATE nextState)
    {
        if (nextState == UISTATE.ACTIVE&&_nowUIState!=UISTATE.ACTIVE)
        {
            ActiveInitAction();
        }
        _nowUIState = nextState;
    }

    public void ActiveInitAction()
    {
        _isActiveWait.StartWait(0.5f);
        SendMessage2Target();
    }
}
