using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI {
    public class TermMonoActor : SingletonMonoBehaviour<TermMonoActor>
    {
        List<AbstractTransitionTerm> _termList = new List<AbstractTransitionTerm>();

        public void RegisterTerm<T>(T term)
            where T:AbstractTransitionTerm
        {
            if (!_termList.Contains(term))
            {
                _termList.Add(term);
                term.InitAction();
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
