using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace aojiru_UI
{
    public abstract class AbstractTransitionTerm<T, K>
        where T : TransitionLine<K>
    {
        [SerializeField] T _transitionLine = (T)new TransitionLine<K>();

        public bool _enable { get; private set; }


        public AbstractTransitionTerm()
        {

        }

        public AbstractTransitionTerm(K from, K to)
        {
            _transitionLine.SetFrom(from);
            _transitionLine.SetTo(to);
        }

        public void SetFrom(K from)
        {
            _transitionLine.SetFrom(from);
        }

        public void SetTo(K to)
        {
            _transitionLine.SetTo(to);
        }

        public K GetTo()
        {
            return _transitionLine.GetTo();
        }
        public bool ActiveTerm(K nowNode)
        {
            bool result = _transitionLine.GetFrom().Equals(nowNode);
            if (result)
            {
                if (!_enable) EnableAction();
            }
            else
            {
                if (_enable) DisableAction();
            }
            _enable = result;
            return result;
        }


        public abstract bool MeetTerm();

        public virtual void AwakeAction() { }
        public virtual void StartAction() { }
        public virtual void UpdateAction() { }

        protected virtual void EnableAction() { }
        protected virtual void DisableAction() { }
    }
    [System.Serializable]
    public class OnclickTransition<T> : AbstractTransitionTerm<TransitionLine<T>, T>
    {
        bool pushed = false;
        [SerializeField] List<GameObjectSaver> pushButtonList = new List<GameObjectSaver>();

        public OnclickTransition(T from, T to) : base(from, to)
        {
        }

        public void AddButton(GameObject obj)
        {
            var add = InstanceIdHolder.AddIdHolder(obj);
            pushButtonList.Add(new GameObjectSaver(add));
        }

        public override bool MeetTerm()
        {
            return pushed;
        }

        protected override void EnableAction()
        {
            base.EnableAction();
            pushed = false;
        }

        public override void StartAction()
        {
            base.StartAction();
            foreach (var bt in pushButtonList)
            {
                bt.GetObj().GetComponent<Button>().onClick.AddListener(() => pushed = true);
            }
        }
    }
}
