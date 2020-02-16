using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;

namespace aojiru_UI
{
    public class TrrigerTermNodeSet : NodeSet<TrrigerTermNodeData>
    {

        public TrrigerTermNodeSet(UITermWIndow parentWindow,Vector2 firstPos, Vector2 nodeSize
            ,AbstractUITrrigerTerm trrigerTerm
            , int colorCode = 0)
            : base(parentWindow,firstPos, nodeSize, colorCode)
        {
            RawAddNode(new TrrigerTermNodeData(this,trrigerTerm));
        }

        public override void AddNode()
        {
            throw new System.NotImplementedException();
        }

        protected override void RawRemoveNode(TrrigerTermNodeData data)
        {
            base.RawRemoveNode(data);
        }
    }

    public class TrrigerTermNodeData : NodeData
    {
        TrrigerType _trrigerType;

        
        AbstractUITrrigerTerm _trrigerTerm;
        TrrigerTermNodeSet _nodeSet;
        

        public TrrigerTermNodeData(TrrigerTermNodeSet nodeSet,AbstractUITrrigerTerm trrigerTerm)
        {
            _nodeSet = nodeSet;
            if (_trrigerTerm == null)
            {
                //_tranTerm.AddTrrigerTerm(TrrigerType.None);
                _trrigerTerm = new NoneUITrrigerTerm();
            }

            _trrigerType = _trrigerTerm.GetTrrigerType();

        }

        public override void AbstractCallBack()
        {
            EditorGUI.BeginChangeCheck();
            _trrigerType = (TrrigerType)EditorGUILayout.EnumPopup("Type", _trrigerType);
            if (EditorGUI.EndChangeCheck())
            {
                //trantermの情報とどちらも修正できるか怪しい
                _trrigerTerm = TrrigerTypeConstract.ConstractTerm(_trrigerType);
                
            }

            switch (_trrigerType)
            {
                case TrrigerType.None:
                    break;
                case TrrigerType.Onclick:
                    {
                        OncliclUITrrigerTerm term = (OncliclUITrrigerTerm)_trrigerTerm;
                        term._HeadButton = EditorGUILayout.ObjectField("ボタン", term._HeadButton, typeof(Button), true) as Button;
                        break;
                    }
            }
        }
    }
}
#endif
