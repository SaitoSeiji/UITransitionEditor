using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aojiru_UI;
using UnityEditor.Experimental.GraphView;
using System.Linq;

namespace aoji_EditorUI
{
    public class SaveBuildePrepare
    {

        public Dictionary<int, GameObject> stateList;
        public List<(Edge key,int from, int to, bool active)> lineConnectList = new List<(Edge key,int from, int to, bool active)>();
        public List<(UITransitionTerm term, int line)> linesTermData = new List<(UITransitionTerm term, int line)>();

        public void PrepareSave(UIBaseGraphView graphView, EdgeDataList edgeData)
        {
            SetState(graphView);
            SetLine(graphView,edgeData);
            SetTerm(edgeData);
        }

        void SetState(UIBaseGraphView graphView)
        {
            stateList = new Dictionary<int, GameObject>();
            for(int i = 0; i<graphView._nodeList.Count; i++)
            {
                stateList.Add(i, graphView._nodeList[i].obj);
            }
        }

        void SetLine(UIBaseGraphView graphView,EdgeDataList edgeData)
        {
            lineConnectList = new List<(Edge key,int from, int to, bool active)>();
            var processedEdge = new List<Edge>();
            //処理は最適化すれば軽くできるはず（困ってないのでいつかやる）
            //すべてのedgeに重複なく処理をするためにforeaceとprocessedEdgeを使用している
            foreach (var port in graphView.ports.ToList())
            {
                foreach (var edge in port.connections)
                {
                    if (!processedEdge.Contains(edge))
                    {
                        processedEdge.Add(edge);

                        var nodeData = graphView.GetArrowNode(edge);
                        int toIndex = graphView._nodeList.IndexOf(nodeData.to);
                        int fromIndex = graphView._nodeList.IndexOf(nodeData.from);
                        bool active = edgeData.GetActive(edge);
                        lineConnectList.Add((edge,fromIndex, toIndex, active));
                    }
                }
            }
        }

        void SetTerm(EdgeDataList edgeData)
        {
            linesTermData = new List<(UITransitionTerm term, int line)>();
            for(int i = 0; i < lineConnectList.Count; i++)
            {
                var addItem = new UITransitionTerm();
                linesTermData.Add((addItem, i));

                var edge = lineConnectList[i].key;
                var termList=edgeData.GetList(edge);
                
                foreach(var term in termList)
                {
                    if(term is BoolNode)
                    {
                        addItem.AddBool(term.GetTerm() as AbstractUIBoolTerm);
                    }else if(term is TrrigerNode)
                    {
                        addItem.SetTrriger(term.GetTerm() as AbstractUITrrigerTerm);
                    }
                }

                //トリガーがないなら空を入れておく
                if (addItem._trrigerTerm == null)
                {
                    addItem.SetTrriger(new NoneUITrrigerTerm());
                }
            }
        }

        public void TestSetState()
        {
            foreach (var state in stateList)
            {
                Debug.Log("key:" + state.Key + " | value:" + state.Value);
            }
        }
        public void TestSetLine()
        {
            foreach (var line in lineConnectList)
            {
                Debug.Log(" | from:" + line.from + " | to:" + line.to+ " | active:" + line.active);
            }
        }

        public void TestSetTerm()
        {
            for(int i = 0; i < lineConnectList.Count; i++)
            {
                string log = "index:" + i + " | termsLineNumber:" + linesTermData[i].line + " | term:";
                if(linesTermData[i].term._trrigerTerm!=null)
                    log +="-trriger:"+ linesTermData[i].term._trrigerTerm.GetType();
                if (linesTermData[i].term._boolTerms != null)
                {
                    foreach (var term in linesTermData[i].term._boolTerms)
                    {
                        log += " -bool:" + term.GetType();
                    }
                }
                Debug.Log(log);
            }
        }
    }
}
