using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace aoji_EditorUI
{
    public class SanpleGraphView : GraphView
    {
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
                AddElement(new SampleNode());
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

        public bool a()
        {
            bool result = false;
            foreach (var sel in selection)
            {
                if (sel.GetType() == typeof(Edge))
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
