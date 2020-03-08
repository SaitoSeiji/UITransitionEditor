using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace aoji_EditorUI
{
    public class UIBaseGraphView : GraphView
    {
        public List<UIBaseNode> _nodeList { get; private set; }

        public UIBaseGraphView() : base()
        {
            _nodeList = new List<UIBaseNode>();
            //ズーム機能の追加
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            //背景色の変更
            Insert(0, new GridBackground());

            //ドラッグして動かせるようにした
            this.AddManipulator(new SelectionDragger());

            //右クリックで追加できるようになった
            nodeCreationRequest += context =>
            {
                var newNode = new UIBaseNode();
                AddElement(newNode);
                _nodeList.Add(newNode);
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

        public (UIBaseNode from, UIBaseNode to) GetArrowNode(Edge target)
        {

            UIBaseNode from = null;
            UIBaseNode to = null;
            foreach (var port in ports.ToList())
            {
                foreach (var edge in port.connections)
                {
                    if (edge == target)
                    {
                        var node = port.node as UIBaseNode;
                        if (port.direction == Direction.Output)
                        {
                            from = node;
                        }
                        else
                        {
                            to = node;
                        }
                    }
                }
            }
            return (from, to);
        }

        public bool IsSelectEdge()
        {
            return GetSelectEdge() != null;
        }

        public Edge GetSelectEdge()
        {
            if (selection.Count != 1) return null;
            var result = selection[0] as Edge;
            return result;
        }
    }
}
