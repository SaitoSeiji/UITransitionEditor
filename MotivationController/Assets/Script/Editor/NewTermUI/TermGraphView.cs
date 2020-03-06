using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace aoji_EditorUI
{

    public class TermGraphView : GraphView
    {
        public List<TermNode> _nodeList = new List<TermNode>();

        Rect firstRect = new Rect(0,0,200,200);

        NodeWindow window;

        public TermGraphView(NodeWindow win) : base()
        {
            window = win;
            //ズーム機能の追加
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            //背景色の変更
            //Insert(0, new GridBackground().);

            //ドラッグして動かせるようにした
            //this.AddManipulator(new SelectionDragger());

            //右クリックで追加できるようになった
            nodeCreationRequest += context =>
            {
                if (!window.graphView.IsSelectArrow()) return;
                var newNode = new TermNode();
                AddNode(newNode);
            };
        }

        void AddNode(TermNode node)
        {
            AddElement(node);
            _nodeList.Add(node);
            node.SetPosition(GetCorrectNodeRect(_nodeList.Count - 1));
        }
        
        Rect GetCorrectNodeRect(int index)
        {
            float xInterval = 150;
            try
            {
                Rect beforeRect = _nodeList[index - 1].GetPosition();
                return new Rect(beforeRect.x + xInterval, firstRect.y, firstRect.width,firstRect.height);
            }
            catch(System.ArgumentException ex)
            {
                return firstRect;
            }
        }

        public override EventPropagation DeleteSelection()
        {

            if (_nodeList.Contains(selection[0] as TermNode))
            {
                _nodeList.Remove(selection[0] as TermNode);
                for (int i = 0; i < _nodeList.Count; i++)
                {
                    _nodeList[i].SetPosition(GetCorrectNodeRect(i));
                }
            }
            return base.DeleteSelection();
        }

        public void ClearList()
        {
            foreach(var node in _nodeList)
            {
                RemoveElement(node);
            }
            _nodeList = new List<TermNode>();
        }

        public void SetNodeList(List<TermNode> newNodeList)
        {
            _nodeList = new List<TermNode>();

            for (int i = 0; i < newNodeList.Count; i++)
            {
                AddNode(newNodeList[i] as TermNode);
            }
        }
    }
}
