using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace aojiru_UI
{
    public abstract class AbstractTransitonController<KEY,LINE,TERM> : MonoBehaviour
        where LINE : AbstractTransitionLine<KEY,TERM>
        where TERM : AbstractTransitionTerm
    {
        List<LINE> _transitionLineList;

        protected KEY _nowKey { get; private set; }

        protected abstract List<LINE> InitTransitionTerm();
        protected abstract KEY SetFirstKey();

        protected virtual void Awake()
        {
            _transitionLineList=InitTransitionTerm();
            _nowKey = SetFirstKey();
        }
        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            foreach (var line in _transitionLineList)
            {
                if (line.IsActive(_nowKey))
                {
                    if (line.PermitTransition())
                    {
                        KeyChengeAction(line);
                        break;
                    }
                }
            }
        }

        protected virtual void KeyChengeAction(LINE line)
        {
            _nowKey = line.GetTo();
        }
    }
}
