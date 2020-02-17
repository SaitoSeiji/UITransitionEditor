using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    [SerializeField]
    public class TransitionLine<T>
    {
        [SerializeField]protected T _from;
        [SerializeField]protected T _to;

        [SerializeField] int _fromInstance;
        [SerializeField] int _toInstance;
        [SerializeField] bool _isMono;

        public virtual T GetFrom()
        {
            if (_isMono)
            {
                _from = InstanceIdCash.Instance.GetId(_fromInstance).gameObject.GetComponent<T>();
            }
            return _from;
        }
        public virtual T GetTo()
        {
            if (_isMono)
            {
                _to = InstanceIdCash.Instance.GetId(_toInstance).gameObject.GetComponent<T>();
            }
            return _to;
        }

        public virtual void SetFrom(T data)
        {
            MonoBehaviour d = data as MonoBehaviour;
            if (d != null)
            {
                _fromInstance = SetInstanceId(d);
            }
            _from = data;
        }

        public virtual void SetTo(T data)
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
    }
}

