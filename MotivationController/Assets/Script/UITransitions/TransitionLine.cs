using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace aojiru_UI
{
    [SerializeField]
    public class AbstractTransitionLine
    {
        [SerializeField] public TransitionState _nextState { get; private set; }
        [SerializeField] AbstractTransitionTerm _transitionTerm;
        

        #region アクセス関連
        public void SetTo(TransitionState data)
        {
            _nextState = data;
        }
        public void SetTerm(AbstractTransitionTerm term)
        {
            _transitionTerm = term;
        }
        
        #endregion
        
        public bool PermitTransition()
        {
            return _transitionTerm.MeetTerm();
        }

        public void SetEnable(bool enable)
        {
            _transitionTerm.SetEnable(enable);
        }
    }

    [SerializeField]
    public class UITransitonTermLine : AbstractTransitionLine
    {
        [SerializeField] bool _selfActive;
        public bool _SelfActive { get { return _selfActive; } }
        public UITransitonTermLine(bool selfActive)
        {
            _selfActive = selfActive;
        }
    }
}

