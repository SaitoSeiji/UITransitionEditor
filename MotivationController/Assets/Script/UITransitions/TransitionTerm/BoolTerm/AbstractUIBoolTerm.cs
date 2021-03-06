﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{

    //UIの遷移条件　bool条件
    public abstract class AbstractUIBoolTerm : AbstractTransitionTerm
    {
        public override bool MeetTerm()
        {
            return ConcreteTerm();
        }

        protected abstract bool ConcreteTerm();
    }
}
