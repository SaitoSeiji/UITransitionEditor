using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aojiru_UI;
using DataSaver;

public class CanvasBaseCtrl : MonoBehaviour
{
    #region builder
    [SerializeField] TestBuilderMono_case2 initer;
    [SerializeField] TestBuilderMono_case3 initer2;
    #endregion 

    TransitionState _nowState;
    MonoTranBoard _myBoard;
    TransitionPin _tranCtrl;
    DefaultCanvasImpl _defaultImpl;

    private void Start()
    {
        if (initer2.LoadEnable())
        {
            _myBoard = (MonoTranBoard)initer2.CreateBoard();
            Debug.Log("load");
        }
        else
        {
            _myBoard = (MonoTranBoard)initer.CreateBoard();
        }

        _tranCtrl = _myBoard.CreateCtrl();
        _tranCtrl.AwakeFirstState();
        _nowState = _tranCtrl._nowState;
        _defaultImpl = new DefaultCanvasImpl(GetCanvasFromState(_nowState));
    }

    private void Update()
    {
        _tranCtrl.StateAction();
        if (_tranCtrl.ChengedState)
        {
            ChengeStateAction(_nowState, _tranCtrl._nowState);
            _nowState = _tranCtrl._nowState;
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
        return _myBoard.GetObject(state).GetComponent<UICanvasBase>();
    }

    [ContextMenu("save")]
    public void SaveAction()
    {
        _myBoard = (MonoTranBoard)initer.CreateBoard();
        FullSerializSaver.SaveAction(_myBoard, initer.saveKey);
    }
}
