using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI {
    public class TermMonoActor : SingletonMonoBehaviour<TermMonoActor>
    {
        List<AbstractTransitionTerm> _termList = new List<AbstractTransitionTerm>();
        bool startIsEnd = false;

        public void RegisterTerm<T>(T term)
            where T:AbstractTransitionTerm
        {
            if (!_termList.Contains(term))
            {
                _termList.Add(term);
                if(startIsEnd) term.InitAction();
            }
        }

        private void Start()
        {
            startIsEnd = true;
            for (int i = 0; i < _termList.Count; i++)
            {
                _termList[i].InitAction();
            }
        }

        private void Update()
        {
            for(int i = 0; i < _termList.Count; i++)
            {
                _termList[i].UpdateAction();
            }
        }
    }
}
