using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    public class TransitionPin
    {
        public TransitionState _nowState { get; private set; }

        TransitionBoard _myBoard;

        Trriger _chengedState = new Trriger();
        public bool ChengedState { get { return _chengedState._Trriger; } }

        public TransitionPin(TransitionBoard myBoard)
        {
            _myBoard = myBoard;
            _nowState = myBoard.FirstState;
        }

        public void AwakeFirstState()
        {
            _nowState.SetActive(true);
        }

        public void StateAction()
        {
            var nextState= _nowState.GetNextState();
            
            if(nextState!=_nowState)
            {
                _nowState.SetActive(false);
                nextState.SetActive(true);
                _nowState = nextState;
                _chengedState._Trriger = true;
            }
        }
    }
}
