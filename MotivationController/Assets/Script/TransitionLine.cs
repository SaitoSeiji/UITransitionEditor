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

        public virtual T GetFrom()
        {
            return _from;
        }
        public virtual T GetTo()
        {
            return _to;
        }

        public virtual void SetFrom(T data)
        {
            _from = data;
        }

        public virtual void SetTo(T data)
        {
            _to = data;
        }
    }

    public class UICanvasTransition: TransitionLine<UICanvasBase>
    {
        [SerializeField] int _fromInstance;
        [SerializeField] int _toInstance;

        public override UICanvasBase GetFrom()
        {
            var result = base.GetFrom();
            if (result == null)
            {
                _from = InstanceIdCash.Instance.GetCanvas(_fromInstance);
                result = _from;
            }
            return result;
        }
        public override UICanvasBase GetTo()
        {
            var result = base.GetTo();
            if (result == null)
            {
                _to = InstanceIdCash.Instance.GetCanvas(_toInstance);
                result = _to;
            }
            return result;
        }

        public override void SetFrom(UICanvasBase data)
        {
            _fromInstance = data.gameObject.GetInstanceID();
            base.SetFrom(data);
        }

        public override void SetTo(UICanvasBase data)
        {
            _toInstance = data.gameObject.GetInstanceID();
            base.SetTo(data);
        }
    }
}

