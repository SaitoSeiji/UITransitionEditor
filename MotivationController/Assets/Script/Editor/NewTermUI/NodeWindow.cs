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
        [SerializeField] Dictionary<Edge, (List<TermNode> term,bool active)> _edgeData = new Dictionary<Edge, (List<TermNode>,bool)>();


        public void StartUpDic(Edge select)
        {
            if (_edgeData.ContainsKey(select))
            {
            }
            else
            {
                _edgeData.Add(select, (new List<TermNode>(),false));
            }
        }

        public void UpdateDic(Edge select,List<TermNode> nodeList,bool active)
        {
            _edgeData[select] = (nodeList,active);
        }

        public List<TermNode> GetList(Edge key)
        {
            return _edgeData[key].term;
        }

        public bool GetActive(Edge key)
        {
            return _edgeData[key].active;
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
                    _edgeDataList.UpdateDic(_selectEdge.beforeValue,_termGraphView._nodeList,_termGraphView.IsActive);
                }
                ResetPanelContent();
                if (_selectEdge.nowValue != null)
                {
                    _edgeDataList.StartUpDic(_selectEdge.nowValue);
                    SetPanelContent(_selectEdge.nowValue);

                    //var nowNodeObj = _uiBaseGraphView.GetNowArrowsNodeObj();
                    //Debug.Log(nowNodeObj.input.name + ":" + nowNodeObj.output.name);
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
            _termGraphView.SetNodeList(_edgeDataList.GetList(select),_edgeDataList.GetActive(select));
        }
        


        void applyButtonAction()
        {
            Debug.Log("apply:まだ実装してないよ");
            var savePrepare = new SaveBuildePrepare();
            savePrepare.PrepareSave(_uiBaseGraphView, _edgeDataList);
            var savebuilder = new TestSaveBuilder_case1(savePrepare);
            var board = savebuilder.CreateBoard();
            DataSaver.FullSerializSaver.SaveAction(board,"editorSave_test");
            //savePrepare.TestSetState();
            //savePrepare.TestSetLine();
            //savePrepare.TestSetTerm();
        }
    }
}
