using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    [SerializeField]
    public class AbstractTransitionLine<STATE,TERM>
        where TERM : AbstractTransitionTerm
    {
        [SerializeField] STATE _from;
        [SerializeField] STATE _to;

        [SerializeField] int _fromInstance;
        [SerializeField] int _toInstance;
        [SerializeField] bool _isMono;

        [SerializeField] TERM _transitionTerm;

        

        #region アクセス関連
        public STATE GetFrom()
        {
            if (_isMono)
            {
                _from = InstanceIdCash.Instance.GetId(_fromInstance).gameObject.GetComponent<STATE>();
            }
            return _from;
        }
        public STATE GetTo()
        {
            if (_isMono)
            {
                _to = InstanceIdCash.Instance.GetId(_toInstance).gameObject.GetComponent<STATE>();
            }
            return _to;
        }

        public void SetFrom(STATE data)
        {
            MonoBehaviour d = data as MonoBehaviour;
            if (d != null)
            {
                _fromInstance = SetInstanceId(d);
            }
            _from = data;
        }

        public void SetTo(STATE data)
        {
            MonoBehaviour d = data as MonoBehaviour;
            if (d != null)
            {
                _toInstance = SetInstanceId(d);
            }
            _to = data;
        }

        int SetInstanceId(MonoBehaviour mono)
        {
            _isMono = true;
            var id=InstanceIdHolder.AddIdHolder(mono.gameObject);
            return id.GetInstanceID();
        }

        public void SetTerm(TERM term)
        {
            _transitionTerm = term;
        }
        #endregion
        public virtual bool IsActive(STATE state)
        {

            bool result = state.Equals(GetFrom());
            _transitionTerm.SetEnable(result);
            return result;
        }

        public bool PermitTransition()
        {
            return _transitionTerm.MeetTerm();
        }
    }

    [SerializeField]
    public class UITransitonTermLine<STATE> : AbstractTransitionLine<STATE, UITransitionTerm>
    {
        [SerializeField] bool _selfActive;
        public bool _SelfActive { get { return _selfActive; } }
        public UITransitonTermLine(bool selfActive)
        {
            _selfActive = selfActive;
        }
    }
}

