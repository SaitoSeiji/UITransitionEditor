using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace aoji_EditorUI
{
    public class BoolNode : TermNode
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
    }
}
