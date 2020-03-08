using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using aojiru_UI;

namespace aoji_EditorUI
{
    public abstract class TrrigerNode : TermNode
    {
    }

    public class NoneTrrigerNode : TrrigerNode
    {
        public NoneTrrigerNode() : base()
        {
            title = "NoneTrrigerTerm";
        }

        public override AbstractTransitionTerm GetTerm()
        {
            return new NoneUITrrigerTerm();
        }
    }

    public class OnclickTrrigerNode : TrrigerNode
    {

        ObjectField btInput;
        public UnityEngine.UI.Button button { get { return btInput.value as UnityEngine.UI.Button; } }
        public OnclickTrrigerNode() : base()
        {

            title = "OnclickTrrigerTerm";
            btInput = new ObjectField();
            btInput.objectType = typeof(UnityEngine.UI.Button);
            mainContainer.Add(btInput);
        }

        public override AbstractTransitionTerm GetTerm()
        {
            var term = new OncliclUITrrigerTerm();
            term.AddButton(button.gameObject);
            return term;
        }
    }
}
