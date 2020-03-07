using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace aoji_EditorUI
{
    public class UIBaseNode : Node
    {
        ObjectField objectInput;
        public GameObject obj { get { return (GameObject)objectInput.value; } }

        public UIBaseNode()
        {
            title = "UIBase";

            var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(Port));
            inputContainer.Add(inputPort);

            var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Port));
            outputContainer.Add(outputPort);
            

            objectInput = new ObjectField();
            objectInput.objectType = typeof(GameObject);
            mainContainer.Add(objectInput);
        }
    }
}
