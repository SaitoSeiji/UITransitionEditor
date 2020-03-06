using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace aoji_EditorUI
{
    public class SanpleGraphView : GraphView
    {
        List<SampleNode> nodeList = new List<SampleNode>();

        public SanpleGraphView() : base()
        {
            //ズーム機能の追加
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            //背景色の変更
            Insert(0, new GridBackground());

            //ドラッグして動かせるようにした
            this.AddManipulator(new SelectionDragger());

            //右クリックで追加できるようになった
            nodeCreationRequest += context =>
            {
                var newNode = new SampleNode();
                AddElement(newNode);
                nodeList.Add(newNode);
            };
        }

        //ノードを接続するための関数
        //ここをいじればつながるかどうかを設定できるはず
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            foreach(var port in ports.ToList())
            {
                if(startPort.node==port.node||
                    startPort.direction==port.direction||
                    startPort.portType != port.portType)
                {
                    continue;
                }
                compatiblePorts.Add(port);
                
            }
            

            return compatiblePorts;
        }

        

        public void Test()
        {
            Debug.Log("hoge");
        }

        public bool IsSelectArrow()
        {
            return GetSelectEdge() != null;
        }

        public (GameObject input,GameObject output) GetNowArrowsNode()
        {
            GameObject input = null;
            GameObject outPut = null;

            var sel = GetSelectEdge();
            if (sel == null) return (null, null);

            foreach (var port in ports.ToList())
            {
                foreach(var edge in port.connections)
                {
                    if (edge == sel)
                    {
                        var node = port.node as SampleNode;
                        if (port.direction == Direction.Input)
                        {
                            input = node.obj;
                        }
                        else
                        {
                            outPut = node.obj;
                        }
                    }
                }
            }
            return (input,outPut);
        }

        public Edge GetSelectEdge()
        {
            if (selection.Count != 1) return null;
            var result = selection[0] as Edge;
            return result;
        }
    }
}
