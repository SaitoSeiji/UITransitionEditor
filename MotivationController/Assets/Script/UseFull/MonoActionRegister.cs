using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace aojiru_UI {
    public class MonoActionRegister : SingletonMonoBehaviour<MonoActionRegister>
    {
        bool startIsEnd = false;

        UnityEvent _awakeAction=new UnityEvent();
        UnityEvent _updateAction=new UnityEvent();

        private void Start()
        {
            startIsEnd = true;
            _awakeAction.Invoke();
        }

        private void Update()
        {
            _updateAction.Invoke();
        }

        public void RegisterAwakeAction(UnityAction ua)
        {
            if (startIsEnd)
            {
                ua.Invoke();
            }
            else
            {
                _awakeAction.AddListener(ua);
            }
        }

        public void RegisterUpdateAction(UnityAction ua)
        {
            _updateAction.AddListener(ua);
        }
    }
}
