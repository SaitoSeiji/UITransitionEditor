using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using aojiru_UI;

namespace aoji_EditorUI
{
    public abstract class BoolNode : TermNode
    {

    }

    public class WaitTimeBoolNode : BoolNode
    {
        FloatField waitTimeField;
        public float waitTime { get { return waitTimeField.value; } }

        public WaitTimeBoolNode() : base()
        {
            title = "WaitTimeBoolTerm";
            waitTimeField = new FloatField("waitTime");
            mainContainer.Add(waitTimeField);

        }

        public override AbstractTransitionTerm GetTerm()
        {
            var term =new AwakeTimeBoolTerm();
            term.SetWaitLength(waitTime);
            return term;
        }
    }
}
