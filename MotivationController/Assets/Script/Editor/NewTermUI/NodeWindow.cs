using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace aoji_EditorUI
{
    [System.Serializable]
    public class EdgeDataList
    {
        [SerializeField]Dictionary<Edge, List<TermNode>> _edgeData = new Dictionary<Edge, List<TermNode>>();


        public void StartUpDic(Edge select)
        {
            if (_edgeData.ContainsKey(select))
            {
            }
            else
            {
                _edgeData.Add(select, new List<TermNode>());
            }
        }

        public void UpdateDic(Edge select,List<TermNode> nodeList)
        {
            _edgeData[select] = nodeList;
        }

        public List<TermNode> GetList(Edge key)
        {
            return _edgeData[key];
        }
    }

    public class NodeWindow : DefaultWindow
    {
        public static void OpenWindow()
        {
            ShowWindow<NodeWindow>();
        }

        public UIBaseGraphView _uiBaseGraphView { get; private set; }
        TermGraphView _termGraphView;
        
        UpdateNowBefore<Edge> _selectEdge=new UpdateNowBefore<Edge>();
        EdgeDataList _edgeDataList = new EdgeDataList();
        private void OnGUI()
        {
            _selectEdge.UpdateNowValue(_uiBaseGraphView.GetSelectEdge());
            if (_selectEdge.SwitchTrriger)
            {
                if (_selectEdge.beforeValue != null)
                {
                    _edgeDataList.UpdateDic(_selectEdge.beforeValue,_termGraphView._nodeList);
                }
                ResetPanelContent();
                if (_selectEdge.nowValue != null)
                {
                    _edgeDataList.StartUpDic(_selectEdge.nowValue);
                    SetPanelContent(_selectEdge.nowValue);
                }
            }
        }

        override protected void OnEnable()
        {
            base.OnEnable();

            //applyボタンの生成
            var bt = new Button(()=>applyButtonAction());
            bt.text = "apply";
            rootVisualElement.Add(bt);

            //uiBaseGraphViewの生成
            _uiBaseGraphView = new UIBaseGraphView()
            {
                style = { flexGrow = 1 }
            };
            rootVisualElement.Add(_uiBaseGraphView);

            //_termGraphViewの生成
            _termGraphView = new TermGraphView(this)
            {
                style = { flexGrow = 1 }
            };
            rootVisualElement.Add(_termGraphView);

        }
        
        void ResetPanelContent()
        {
            _termGraphView.ClearList();
        }

        void SetPanelContent(Edge select)
        {
            _termGraphView.SetNodeList(_edgeDataList.GetList(select));
        }
        


        void applyButtonAction()
        {
            Debug.Log("apply:まだ実装してないよ");
            var test = new BoardCreator_fromEditor();
            test.SetState(_uiBaseGraphView);
            test.TestSetState();
        }
    }
}
