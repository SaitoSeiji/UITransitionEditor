using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace aoji_EditorUI
{

    public class TermGraphView : GraphView
    {
        public List<TermNode> _nodeList { get; private set; }

        Rect firstRect = new Rect(0,0,200,200);

        NodeWindow _myWindow;

        public TermGraphView(NodeWindow win) : base()
        {
            _nodeList = new List<TermNode>();

            _myWindow = win;
            //ズーム機能の追加
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            
            //作るノードの種類を選べる機能の追加
            var searchWindowProvider = new SampleSearchWindowProvider();
            searchWindowProvider.Initialize(this);
            nodeCreationRequest += context =>
            {
                bool makeTermEnable = _myWindow._uiBaseGraphView.IsSelectEdge();
                if (!makeTermEnable) return;
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindowProvider);
            };
        }

        public override EventPropagation DeleteSelection()
        {

            if (_nodeList.Contains(selection[0] as TermNode))
            {
                RemoveNode(selection[0] as TermNode);
            }
            return base.DeleteSelection();
        }

        public void AddNode(TermNode node)
        {
            AddElement(node);
            _nodeList.Add(node);
            node.SetPosition(GetCorrectNodeRect(_nodeList.Count - 1));
        }
        void RemoveNode(TermNode node)
        {
            _nodeList.Remove(node);
            UpdateNodeRect();
        }
        
        //ノードの位置関連
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
        void UpdateNodeRect()
        {
            for (int i = 0; i < _nodeList.Count; i++)
            {
                _nodeList[i].SetPosition(GetCorrectNodeRect(i));
            }
        }

        //リストの制御
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
