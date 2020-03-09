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
        MonoTranBoard_test result;
        var boardBuilder = new BoardBuilder<MonoTranBoard_test>();
        if (initer2.LoadEnable())
        {
            boardBuilder.PrepareData(initer2);
            result=boardBuilder.CreateBoard();
            Debug.Log("load");
        }
        else
        {
            boardBuilder.PrepareData(initer);
            result = boardBuilder.CreateBoard();
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
        State_UIBase stateNew = (State_UIBase)state;
        return stateNew.UIBase.GetComponent<UICanvasBase>();
    }

    [ContextMenu("save")]
    public void SaveAction()
    {
        var boardBuilder = new BoardBuilder<MonoTranBoard_test>();
        boardBuilder.PrepareData(initer);
        _tranBoard = boardBuilder.CreateBoard();
        FullSerializSaver.SaveAction(_tranBoard, initer.saveKey);
    }
}
