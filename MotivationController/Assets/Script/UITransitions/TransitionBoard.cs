using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace aojiru_UI
{
    [System.Serializable]
    public class TransitionBoard
    {
        [SerializeField]protected List<TransitionState> _stateList = new List<TransitionState>();
        public List<TransitionState> _StateList { get { return _stateList; } }
        [SerializeField]int _firstStateIndex = 0;
        public TransitionState FirstState { get { return _stateList[_firstStateIndex]; } }

        [SerializeField]protected List<AbstractTransitionLine> _lineList = new List<AbstractTransitionLine>();
        public List<AbstractTransitionLine> _LineList { get { return _lineList; } }

        public TransitionPin CreatePin()
        {
            return new TransitionPin(this);
        }
        
        #region add
        public void AddState(TransitionState state)
        {
            if (_stateList.Contains(state)) return;
            _stateList.Add(state);
        }

        public void AddLine(AbstractTransitionLine line)
        {
            if (_lineList.Contains(line)) return;
            _lineList.Add(line);
        }

        
        #endregion
        #region Set
        public static void SetLineFromTo(TransitionState from, TransitionState to, AbstractTransitionLine line)
        {
            line.SetTo(to);
            from.AddLineList(line);
        }
        public void SetLineFromTo(int from, int to, int line)
        {
            SetLineFromTo(_stateList[from], _stateList[to], _lineList[line]);
        }
        

        public void SetTerm(AbstractTransitionTerm term, AbstractTransitionLine line)
        {
            line.SetTerm(term);
        }
        public void SetTerm(AbstractTransitionTerm term, int line)
        {
            SetTerm(term, _lineList[line]);
        }
        #endregion
    }
    //[System.Serializable]
    //public class MonoTranBoard : TransitionBoard
    //{
    //    [SerializeField] Dictionary<TransitionState, int> _instanceIdDic = new Dictionary<TransitionState, int>();

    //    public void SetObject(TransitionState state, GameObject obj)
    //    {
    //        var holder = InstanceIdHolder.AddIdHolder(obj);
    //        _instanceIdDic.Add(state, holder.GetInstanceID());
    //    }
    //    public void SetObject(int state, GameObject obj)
    //    {
    //        SetObject(_stateList[state], obj);
    //    }

    //    public GameObject GetObject(TransitionState state)
    //    {
    //        var cash = InstanceIdCash.Instance.GetId(_instanceIdDic[state]);
    //        return cash.gameObject;
    //    }
    //}
    [System.Serializable]
    public class MonoTranBoard_test : TransitionBoard
    {
        public static void SetObject(State_UIBase state, GameObject obj)
        {
            var holder = InstanceIdHolder.AddIdHolder(obj);
            state.SetUIBase(obj);
            state.SetBaseId(holder.GetInstanceID());
        }
    }
}
