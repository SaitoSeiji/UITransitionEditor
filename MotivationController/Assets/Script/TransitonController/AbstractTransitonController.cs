using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace aojiru_UI
{
    public abstract class AbstractTransitonController<T, KEY> : MonoBehaviour
        where T : AbstractTransitionTerm<TransitionLine<KEY>, KEY>
    {
        protected List<T> _transitionTermList;

        protected KEY _nowKey { get; private set; }

        protected abstract void InitTransitionTerm();
        protected abstract KEY SetFirstKey();

        protected virtual void Awake()
        {
            InitTransitionTerm();
            _nowKey = SetFirstKey();
            foreach (var term in _transitionTermList)
            {
                term.AwakeAction();
            }
        }

        protected virtual void Start()
        {
            foreach (var term in _transitionTermList)
            {
                term.StartAction();
            }
        }

        protected virtual void Update()
        {
            foreach (var term in _transitionTermList)
            {
                if (term._enable) term.UpdateAction();


                if (term.ActiveTerm(_nowKey))
                {
                    if (term.MeetTerm())
                    {
                        KeyChengeAction(term);
                    }
                }
            }
        }

        protected virtual void KeyChengeAction(T term)
        {
            _nowKey = term.GetTo();
        }
    }
}
