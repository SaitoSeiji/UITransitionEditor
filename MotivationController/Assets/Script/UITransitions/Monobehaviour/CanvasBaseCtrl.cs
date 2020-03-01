using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aojiru_UI;
using DataSaver;

public class CanvasBaseCtrl : BoardMonoBehaviour
{
    #region builder
    [SerializeField] TestBuilderMono_case2 initer;
    [SerializeField] TestBuilderMono_case3 initer2;
    #endregion 

    TransitionState _nowState;
    DefaultCanvasImpl _defaultImpl;

    protected override TransitionBoard InitBoard()
    {
        //ボードの作成
        MonoTranBoard result;
        if (initer2.LoadEnable())
        {
            result = (MonoTranBoard)initer2.CreateBoard();
            Debug.Log("load");
        }
        else
        {
            result = (MonoTranBoard)initer.CreateBoard();
        }
        return result;
    }

    protected override void Start()
    {
        base.Start();

        _nowState = _tranPin._nowState;
        _defaultImpl = new DefaultCanvasImpl(GetCanvasFromState(_nowState));
    }

    protected override void Update()
    {
        base.Update();
        if (_tranPin.ChengedState)
        {
            ChengeStateAction(_nowState, _tranPin._nowState);
            _nowState = _tranPin._nowState;
        }
    }
    

    void ChengeStateAction(TransitionState before, TransitionState next)
    {
        var line=(UITransitonTermLine)before._permitLine;
        if (line._SelfActive)
        {
            _defaultImpl.AddCanvas(GetCanvasFromState(next));
        }
        else
        {
            _defaultImpl.CloseCanvas(GetCanvasFromState(next));
            _defaultImpl.AddCanvas(GetCanvasFromState(next));
        }
    }
    
    UICanvasBase GetCanvasFromState(TransitionState state)
    {
        MonoTranBoard myBoard = (MonoTranBoard)_tranBoard;
        return myBoard.GetObject(state).GetComponent<UICanvasBase>();
    }

    [ContextMenu("save")]
    public void SaveAction()
    {
        _tranBoard = (MonoTranBoard)initer.CreateBoard();
        FullSerializSaver.SaveAction(_tranBoard, initer.saveKey);
    }
}
