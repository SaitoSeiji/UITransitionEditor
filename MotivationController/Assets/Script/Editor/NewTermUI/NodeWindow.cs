using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace aoji_EditorUI
{
    public class NodeWindow : DefaultWindow
    {
        public static void OpenWindow()
        {
            ShowWindow<NodeWindow>();
        }

        public SanpleGraphView graphView { get; private set; }
        TermGraphView panel;
        Dictionary<Edge, List<TermNode>> edgeData = new Dictionary<Edge, List<TermNode>>();
        Edge _nowSelectEdge;
        Edge _beforeSelectEdge;
        bool switchNow = false;

        private void OnGUI()
        {
            //現在の選択edgeの取得及びswitchNowの設定
            if (_nowSelectEdge == null)
            {
                if (graphView.IsSelectArrow())
                {
                    _nowSelectEdge = graphView.GetSelectEdge();
                    switchNow = true;
                }
            }
            else
            {
                if (!graphView.IsSelectArrow())
                {
                    _beforeSelectEdge = _nowSelectEdge;
                    _nowSelectEdge = null;
                    switchNow = true;
                }
                else if (_nowSelectEdge != graphView.GetSelectEdge())
                {
                    _beforeSelectEdge = _nowSelectEdge;
                    _nowSelectEdge = graphView.GetSelectEdge();
                    switchNow = true;
                }
            }

            //選択しているedgeが切り替わった時
            if (switchNow)
            {
                if (_beforeSelectEdge != null)
                {
                    UpdateDic(_beforeSelectEdge);
                    _beforeSelectEdge = null;
                }
                ResetPanelContent();

                if (_nowSelectEdge == null)
                {
                }
                else
                {
                    StartUpDic(_nowSelectEdge);
                }
                switchNow = false;
            }
        }

        override protected void OnEnable()
        {
            base.OnEnable();
            graphView = new SanpleGraphView()
            {
                style = { flexGrow = 1 }
            };

            rootVisualElement.Add(graphView);
            panel = new TermGraphView(this)
            {
                style = { flexGrow = 1 }
            };
            rootVisualElement.Add(panel);

        }
        
        void ResetPanelContent()
        {
            panel.ClearList();
        }
        

        void StartUpDic(Edge select)
        {
            if (edgeData.ContainsKey(select))
            {
                panel.SetNodeList(edgeData[select]);
            }
            else
            {
                edgeData.Add(select, new List<TermNode>());
            }
        }

        void UpdateDic(Edge select)
        {
            edgeData[select] = panel._nodeList;
        }
    }
}
