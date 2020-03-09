using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aojiru_UI;
using UnityEditor.Experimental.GraphView;
using System.Linq;

namespace aoji_EditorUI
{
    public class BoardBuilderPrepare_fromEditor:BoardBuilderPrepareMan
    {
        public Dictionary<Node,TransitionState> _stateDic;
        public Dictionary<Edge, UITransitonTermLine> _lineConnectDic;


        protected override void SetStateList()
        {
            _stateList = _stateDic.Values.ToList<TransitionState>();
        }

        protected override void SetLineList()
        {
            _lineConnectList=_lineConnectDic.Select(x => x.Value as AbstractTransitionLine).ToList<AbstractTransitionLine>();
        }

        public void PrepareSaveBoard(UIBaseGraphView graphView, ArrowDataList edgeData)
        {
            SetState(graphView);
            SetLine(graphView, edgeData);
            SetTerm(edgeData);
        }

        void SetState(UIBaseGraphView graphView)
        {
            _stateDic = new Dictionary<Node, TransitionState>();
            var factory = new State_UIBaseFactory();
            for (int i = 0; i < graphView._nodeList.Count; i++)
            {
                var node = graphView._nodeList[i];
                State_UIBase item = (State_UIBase)factory.Create();
                _stateDic.Add(node,item);
                MonoTranBoard_test.SetObject(item,node.obj);
            }
        }

        void SetLine(UIBaseGraphView graphView, ArrowDataList edgeData)
        {
            _lineConnectDic = new Dictionary<Edge, UITransitonTermLine>();
            //処理は最適化すれば軽くできるはず（困ってないのでいつかやる）
            //すべてのedgeに重複なく処理をするためにforeaceとprocessedEdgeを使用している
            foreach (var port in graphView.ports.ToList())
            {
                foreach (var edge in port.connections)
                {
                    if (!_lineConnectDic.ContainsKey(edge))
                    {
                        bool active = edgeData.GetActive(edge);
                        var line = new UITransitonTermLine(active);
                        _lineConnectDic.Add(edge, line);

                        var nodeData = graphView.GetArrowNode(edge);
                        TransitionBoard.SetLineFromTo(_stateDic[nodeData.from],_stateDic[nodeData.to], line);
                    }
                }
            }
        }
        void SetTerm(ArrowDataList edgeData)
        {
            foreach(var item in _lineConnectDic)
            {
                var line = item.Value;
                var addItem = new UITransitionTerm();
                line.SetTerm(addItem);
                var termList = edgeData.GetList(item.Key);
                foreach (var term in termList)
                {
                    if (term is BoolNode)
                    {
                        addItem.AddBool(term.GetTerm() as AbstractUIBoolTerm);
                    }
                    else if (term is TrrigerNode)
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
    }
}
