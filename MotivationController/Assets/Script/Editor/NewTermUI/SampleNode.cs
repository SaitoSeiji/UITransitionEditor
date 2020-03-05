using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace aoji_EditorUI
{
    public class SampleNode : Node
    {
        TextField inputText;
        ObjectField objectInput;

        public SampleNode()
        {
            title = "Sample";

            var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(Port));
            inputContainer.Add(inputPort);

            var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Port));
            outputContainer.Add(outputPort);

            inputText = new TextField();
            mainContainer.Add(inputText);

            objectInput = new ObjectField();
            objectInput.objectType = typeof(GameObject);
            mainContainer.Add(objectInput);
        }
    }
}
