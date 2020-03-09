using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    [System.Serializable]
    public class BoardBuilder<T>
        where T: TransitionBoard,new ()
    {
        List<TransitionState> _stateList;
        List<AbstractTransitionLine> _lineConnectList;

        bool prepared = false;

        public T CreateBoard()
        {
            if (!prepared)
            {
                Debug.Log("BoardBuilder is not prepared");
                return null;
            }

            T _myBoard = new T();
            foreach (var state in _stateList)
            {
                _myBoard.AddState(state);
            }
            foreach (var line in _lineConnectList)
            {
                _myBoard.AddLine(line);
            }
            return _myBoard;
        }

        public void PrepareData(List<TransitionState> stateList, List<AbstractTransitionLine> lineConnectList)
        {
            _stateList = stateList;
            _lineConnectList = lineConnectList;

            prepared = true;
        }
        public void PrepareData(BoardBuilderPrepareMan prepare)
        {
            prepare.Prepare();
            _stateList = prepare._stateList;
            _lineConnectList = prepare._lineConnectList;

            prepared = true;
        }
    }

}
