using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    public class TermMonobehaviour : SingletonMonoBehaviour<TermMonobehaviour>
    {
        [SerializeField] List<AbstractTerm> _abstractTermList=new List<AbstractTerm>();

        protected override void Awake()
        {
            base.Awake();
            RefreshList();
            foreach (var term in _abstractTermList)
            {
                term.AwakeAction();
            }
        }

        private void Start()
        {
            foreach (var term in _abstractTermList)
            {
                term.StartAction();
            }
        }

        private void Update()
        {
            foreach (var term in _abstractTermList)
            {
                term.UpdateAction();
            }
        }

        public void AddList(AbstractTerm newTerm)
        {
            if (!_abstractTermList.Contains(newTerm))
            {
                _abstractTermList.Add(newTerm);
            }
        }

        public void AddList<T>(List<T> termList)
            where T : AbstractTerm
        {
            foreach (var data in termList)
            {
                AddList(data);
            }
        }

        void RefreshList()
        {
            for (int i = _abstractTermList.Count - 1; i >= 0; i--)
            {
                if (_abstractTermList[i] == null)
                {
                    _abstractTermList.RemoveAt(i);
                }
            }
        }
    }
}
