using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using aojiru_UI;

namespace aoji_EditorUI
{
    public abstract class TermNode : Node
    {

        public TermNode()
        {
            
        }

        public abstract AbstractTransitionTerm GetTerm();
    }
}
