using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    public class NoneUITrrigerTerm : AbstractUITrrigerTerm
    {
        protected override bool SetSatisfyAction()
        {
            return true;
        }
        protected override CoalTiming_StaisfyAction SetCoalTiming()
        {
            return CoalTiming_StaisfyAction.UPDATE;
        }
    }
}
