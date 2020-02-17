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


            if (!inited)
            {
                TermMonoActor.Instance.RegisterTerm(this);
                inited = true;
            }
        }

        public abstract bool MeetTerm();

        public virtual void InitAction() { }
        public virtual void UpdateAction() { }
        protected virtual void EnableAction() { }
        protected virtual void DisableAction() { }
    }
    //[System.Serializable]
    //public class OnclickTransition<T> : AbstractTransitionTerm<AbstractTransitionLine<T>, T>
    //{
    //    bool pushed = false;
    //    [SerializeField] List<GameObjectSaver> pushButtonList = new List<GameObjectSaver>();

    //    public OnclickTransition(T from, T to) : base(from, to)
    //    {
    //    }

    //    public void AddButton(GameObject obj)
    //    {
    //        var add = InstanceIdHolder.AddIdHolder(obj);
    //        pushButtonList.Add(new GameObjectSaver(add));
    //    }

    //    public override bool MeetTerm()
    //    {
    //        return pushed;
    //    }

    //    protected override void EnableAction()
    //    {
    //        base.EnableAction();
    //        pushed = false;
    //    }

    //    public override void StartAction()
    //    {
    //        base.StartAction();
    //        foreach (var bt in pushButtonList)
    //        {
    //            bt.GetObj().GetComponent<Button>().onClick.AddListener(() => pushed = true);
    //        }
    //    }
    //}
}
