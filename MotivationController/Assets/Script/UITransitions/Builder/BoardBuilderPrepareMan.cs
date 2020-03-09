using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    public abstract class BoardBuilderPrepareMan
    {
        [System.NonSerialized] public List<TransitionState> _stateList = new List<TransitionState>();
        [System.NonSerialized] public List<AbstractTransitionLine> _lineConnectList = new List<AbstractTransitionLine>();

        public void Prepare()
        {
            SetStateList();
            SetLineList();
        }

        protected virtual void SetStateList() { }
        protected virtual void SetLineList() { }

    }

    public abstract class BoardBuilderPrepareHandInit : BoardBuilderPrepareMan
    {
        protected void SetLineFromTo(int from, int to, int line)
        {
            TransitionBoard.SetLineFromTo(_stateList[from], _stateList[to], _lineConnectList[line]);
        }

        protected void SetTerm(int line, AbstractTransitionTerm term)
        {
            _lineConnectList[line].SetTerm(term);
        }
    }

    public class TestBuilder_case1 : BoardBuilderPrepareHandInit
    {
        protected override void SetStateList()
        {
            var factory = new TransitionStateFactory();
            _stateList.Add((TransitionState)factory.Create());
            _stateList.Add((TransitionState)factory.Create());
            _stateList.Add((TransitionState)factory.Create());
        }

        protected override void SetLineList()
        {
            _lineConnectList.Add(new AbstractTransitionLine());
            _lineConnectList.Add(new AbstractTransitionLine());
            _lineConnectList.Add(new AbstractTransitionLine());

            SetLineFromTo(0, 1, 0);
            SetLineFromTo(1, 2, 1);
            SetLineFromTo(2, 0, 2);


            var term1 = new AwakeTimeBoolTerm();
            term1.SetWaitLength(1);
            var term2 = new AwakeTimeBoolTerm();
            term2.SetWaitLength(2);
            var term3 = new AwakeTimeBoolTerm();
            term3.SetWaitLength(3);


            SetTerm(0, term1);
            SetTerm(1, term2);
            SetTerm(2, term3);
        }

    }

    [System.Serializable]
    public class TestBuilderMono_case1 : BoardBuilderPrepareHandInit
    {
        [SerializeField] GameObject[] objects = new GameObject[3];


        protected override void SetStateList()
        {
            var factory = new State_UIBaseFactory();
            _stateList.Add((State_UIBase)factory.Create());
            _stateList.Add((State_UIBase)factory.Create());
            _stateList.Add((State_UIBase)factory.Create());

            MonoTranBoard_test.SetObject((State_UIBase)_stateList[0], objects[0]);
            MonoTranBoard_test.SetObject((State_UIBase)_stateList[1], objects[1]);
            MonoTranBoard_test.SetObject((State_UIBase)_stateList[2], objects[2]);
        }
        protected override void SetLineList()
        {

            _lineConnectList.Add(new UITransitonTermLine(true));
            _lineConnectList.Add(new UITransitonTermLine(true));
            _lineConnectList.Add(new UITransitonTermLine(false));

            SetLineFromTo(0, 1, 0);
            SetLineFromTo(1, 2, 1);
            SetLineFromTo(2, 0, 2);


            var term1 = new AwakeTimeBoolTerm();
            term1.SetWaitLength(1);
            var term2 = new AwakeTimeBoolTerm();
            term2.SetWaitLength(2);
            var term3 = new AwakeTimeBoolTerm();
            term3.SetWaitLength(3);

            SetTerm(0, term1);
            SetTerm(1, term2);
            SetTerm(2, term3);

        }
    }
    [System.Serializable]
    public class TestBuilderMono_case2 : BoardBuilderPrepareHandInit
    {
        [SerializeField] GameObject[] objects = new GameObject[3];
        [SerializeField] GameObject[] buttons = new GameObject[3];
        public string saveKey;


        protected override void SetStateList()
        {
            var factory = new State_UIBaseFactory();
            _stateList.Add((State_UIBase)factory.Create());
            _stateList.Add((State_UIBase)factory.Create());
            _stateList.Add((State_UIBase)factory.Create());

            MonoTranBoard_test.SetObject((State_UIBase)_stateList[0], objects[0]);
            MonoTranBoard_test.SetObject((State_UIBase)_stateList[1], objects[1]);
            MonoTranBoard_test.SetObject((State_UIBase)_stateList[2], objects[2]);
        }
        protected override void SetLineList()
        {
            _lineConnectList.Add(new UITransitonTermLine(true));
            _lineConnectList.Add(new UITransitonTermLine(true));
            _lineConnectList.Add(new UITransitonTermLine(false));

            SetLineFromTo(0, 1, 0);
            SetLineFromTo(1, 2, 1);
            SetLineFromTo(2, 0, 2);


            var term1 = new UITransitionTerm();
            var term2 = new UITransitionTerm();
            var term3 = new UITransitionTerm();

            SetTerm(0, term1);
            SetTerm(1, term2);
            SetTerm(2, term3);

            var triger1 = new OncliclUITrrigerTerm();
            var triger2 = new OncliclUITrrigerTerm();
            var triger3 = new OncliclUITrrigerTerm();
            triger1.AddButton(buttons[0]);
            triger2.AddButton(buttons[1]);
            triger3.AddButton(buttons[2]);

            term1.SetTrriger(triger1);
            term2.SetTrriger(triger2);
            term3.SetTrriger(triger3);
        }
    }
    [System.Serializable]
    public class TestBuilderMono_case3 : BoardBuilderPrepareMan
    {
        public string saveKey;

        public bool LoadEnable()
        {
            return DataSaver.FullSerializSaver.ExsistFile(saveKey);
        }

        MonoTranBoard_test _loadData;

        protected override void SetStateList()
        {
            _loadData = DataSaver.FullSerializSaver.LoadAction<MonoTranBoard_test>(saveKey);
            _stateList = _loadData._StateList;
        }

        protected override void SetLineList()
        {
            _lineConnectList = _loadData._LineList;
        }

    }
}