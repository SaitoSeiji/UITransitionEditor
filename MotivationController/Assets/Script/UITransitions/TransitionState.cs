using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//factoryMethodを使ってみたが微妙
namespace aojiru_UI
{
    public class TransitionStateFactory : Factory
    {
        List<TransitionState> _stateList = new List<TransitionState>();

        protected override Content CreateMethod()
        {
            return new TransitionState(this);
        }

        protected override void RegisterContent(Content content)
        {
            var newState = (TransitionState)content;
            _stateList.Add(newState);
            newState.SetStateKey(_stateList.Count);
        }

    }

    public class TransitionState : Content
    {
        public string _stateName { get; private set; }
        public int _stateKey { get; private set; }
        bool _isActive = false;
        public AbstractTransitionLine _permitLine { get; private set; }

        [SerializeField]List<AbstractTransitionLine> _myLineList = new List<AbstractTransitionLine>();

        public TransitionState(TransitionStateFactory fact) : base(fact)
        {
            _stateKey = 0;
            _stateName = "";
        }
        #region set
        public void SetName(string stateName)
        {
            _stateName = stateName;
        }

        public void SetStateKey(int key)
        {
            _stateKey = key;
        }

        public void AddLineList(AbstractTransitionLine line)
        {
            _myLineList.Add(line);
        }
        public void RemoveLineList(AbstractTransitionLine line)
        {
            _myLineList.Remove(line);
        }
        #endregion
        public TransitionState GetNextState()
        {
            foreach(var line in _myLineList)
            {
                if (line.PermitTransition())
                {
                    _permitLine = line;
                    return line._nextState;
                }
            }
            return this;
        }

        public void SetActive(bool active)
        {
            _isActive = active;
            foreach(var line in _myLineList)
            {
                line.SetEnable(active);
            }
        }


        //いらないけど継承してるからないといけない
        //消したい
        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}

