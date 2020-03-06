using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace aoji_EditorUI
{
    public class TermNode : Node
    {
        ObjectField btInput;
        public UnityEngine.UI.Button button { get { return btInput.value as UnityEngine.UI.Button; } }

        public TermNode()
        {
            title = "term";
            
            btInput = new ObjectField();
            btInput.objectType = typeof(UnityEngine.UI.Button);
            mainContainer.Add(btInput);
        }

        
    }
}
