using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace aojiru_UI
{
    public abstract class AbstractTransitionTerm
    {

        [System.NonSerialized] bool _isActive = false;
        bool inited = false;
        public void SetEnable(bool nextActive)
        {
            if (nextActive != _isActive)
            {
                if (nextActive) EnableAction();
                else DisableAction();
            }

            _isActive = nextActive;

            RegistarAction();
        }

        public abstract bool MeetTerm();

        public virtual void InitAction() { }
        public virtual void UpdateAction() { }
        protected virtual void EnableAction() { }
        protected virtual void DisableAction() { }

        void RegistarAction()
        {
            if (!inited)
            {
                TermMonoActor.Instance.RegisterTerm(this);
                inited = true;
            }
        }
    }
}
