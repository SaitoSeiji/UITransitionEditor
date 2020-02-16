using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aojiru_UI;
#if UNITY_EDITOR
using UnityEditor;

namespace aojiru_UI
{
    public class UITermWIndow : DefaultWindow
    {
        //windowの情報にアクセスしやすくしたい

        UITransitionTerm _transitionData;

        BoolTermNodeSet _boolNodeSet;
        TrrigerTermNodeSet _trrigerNodeSet;

        public void OpenWindow(UITransitionTerm tranData)
        {

            _transitionData = tranData;
            //_boolNodeSet = new BoolTermNodeSet(new Vector2(400, 50), new Vector2(200, 150),tranData, colorCode: 5);
            _boolNodeSet = new BoolTermNodeSet(this,
                new Vector2(400, 50), new Vector2(200, 150),tranData._boolTerms, colorCode: 5
                );
            _trrigerNodeSet = new TrrigerTermNodeSet(this,
                new Vector2(100, 50), new Vector2(200, 150),tranData._trrigerTerm, colorCode: 3
                );
            ShowWindow<UITermWIndow>();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("apply"))
            {
                SyncData();
            }
            if (GUILayout.Button("add bool term"))
            {
                _boolNodeSet.AddNode();
            }

            BeginWindows();
            //DrawNode(_trrigerNode,"トリガー",0);
            _boolNodeSet.DrawNode("ブール", 10);
            _trrigerNodeSet.DrawNode("トリガー", 0);
            EndWindows();
        }

        void SyncData()
        {
            var termMono = TermMonobehaviour.Instance;
            termMono.AddList(_transitionData._trrigerTerm);
            termMono.AddList(_transitionData._boolTerms);
        }

        protected override Vector2 GetWindowSize()
        {
            return new Vector2(1080, 450);
        }

        #region public
        public AbstractUIBoolTerm SetBoolTerm(AbstractUIBoolTerm term, BoolTermType type)
        {
            if (_transitionData._boolTerms.Contains(term))
            {
                _transitionData._boolTerms.Remove(term);
            }
            var newTerm = BoolTypeConstract.ConstractTerm(type);
            _transitionData._boolTerms.Add(newTerm);
            return newTerm;
        }
        #endregion
    }
}
#endif
