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

        [SerializeField] int _fromInstanceId;
        [SerializeField] int _toInstanceId;
        [SerializeField] bool _isMono;

        [SerializeField] TERM _transitionTerm;
        

        #region アクセス関連
        public STATE GetFrom()
        {
            if (_isMono) _from = FindObjectFromId(_fromInstanceId);
            return _from;
        }
        public STATE GetTo()
        {
            if (_isMono) _to = FindObjectFromId(_toInstanceId);
            return _to;
        }
        public void SetFrom(STATE data)
        {
            if (CheckIsMono(data)) _fromInstanceId = SetInstanceId(data as MonoBehaviour);
            _from = data;
        }
        public void SetTo(STATE data)
        {
            if (CheckIsMono(data)) _toInstanceId = SetInstanceId(data as MonoBehaviour);
            _to = data;
        }
        public void SetTerm(TERM term)
        {
            _transitionTerm = term;
        }
        int SetInstanceId(MonoBehaviour mono)
        {
            _isMono = true;
            var id=InstanceIdHolder.AddIdHolder(mono.gameObject);
            return id.GetInstanceID();
        }

        bool CheckIsMono(STATE data)
        {
            MonoBehaviour d = data as MonoBehaviour;
            return d != null;
        }

        STATE FindObjectFromId(int id)
        {
            return InstanceIdCash.Instance.GetId(id).gameObject.GetComponent<STATE>();
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

