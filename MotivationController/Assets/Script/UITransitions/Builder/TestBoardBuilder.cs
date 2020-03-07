using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace aojiru_UI
{
    [System.Serializable]
    public abstract class TestBoardBuilder
    {
        protected TransitionBoard myBoard;

        public TransitionBoard CreateBoard()
        {
            myBoard = InitBoard();
            AddState();
            AddLine();
            SetLineFromTo();
            SetTerm();
            return myBoard;
        }

        protected abstract TransitionBoard InitBoard();
        protected abstract void AddState();
        protected abstract void AddLine();
        protected abstract void SetLineFromTo();
        protected abstract void SetTerm();

    }

    public class TestBuilder_case1 : TestBoardBuilder
    {
        protected override TransitionBoard InitBoard()
        {
            return new TransitionBoard();
        }

        protected override void AddState()
        {
            var factory = new TransitionStateFactory();
            myBoard.AddState((TransitionState)factory.Create());
            myBoard.AddState((TransitionState)factory.Create());
            myBoard.AddState((TransitionState)factory.Create());
        }

        protected override void AddLine()
        {
            myBoard.AddLine(new AbstractTransitionLine());
            myBoard.AddLine(new AbstractTransitionLine());
            myBoard.AddLine(new AbstractTransitionLine());
        }

        protected override void SetLineFromTo()
        {
            myBoard.SetLineFromTo(0, 1, 0);
            myBoard.SetLineFromTo(1, 2, 1);
            myBoard.SetLineFromTo(2, 0, 2);
        }

        protected override void SetTerm()
        {
            var term1 = new AwakeTimeBoolTerm();
            term1.SetWaitLength(1);
            var term2 = new AwakeTimeBoolTerm();
            term2.SetWaitLength(2);
            var term3 = new AwakeTimeBoolTerm();
            term3.SetWaitLength(3);

            myBoard.SetTerm(term1, 0);
            myBoard.SetTerm(term2, 1);
            myBoard.SetTerm(term3, 2);
        }
    }

    [System.Serializable]
    public class TestBuilderMono_case1 : TestBoardBuilder
    {
        [SerializeField] GameObject[] objects = new GameObject[3];

        protected override TransitionBoard InitBoard()
        {
            return new MonoTranBoard();
        }

        protected override void AddState()
        {
            var factory = new TransitionStateFactory();
            myBoard.AddState((TransitionState)factory.Create());
            myBoard.AddState((TransitionState)factory.Create());
            myBoard.AddState((TransitionState)factory.Create());

            var monoBoard = (MonoTranBoard)myBoard;
            monoBoard.SetObject(0, objects[0]);
            monoBoard.SetObject(1, objects[1]);
            monoBoard.SetObject(2, objects[2]);
        }

        protected override void AddLine()
        {
            var line1 = new UITransitonTermLine(true);
            var line2 = new UITransitonTermLine(true);
            var line3 = new UITransitonTermLine(false);

            myBoard.AddLine(line1);
            myBoard.AddLine(line2);
            myBoard.AddLine(line3);
        }

        protected override void SetLineFromTo()
        {
            myBoard.SetLineFromTo(0, 1, 0);
            myBoard.SetLineFromTo(1, 2, 1);
            myBoard.SetLineFromTo(2, 0, 2);
        }

        protected override void SetTerm()
        {
            var term1 = new AwakeTimeBoolTerm();
            term1.SetWaitLength(1);
            var term2 = new AwakeTimeBoolTerm();
            term2.SetWaitLength(2);
            var term3 = new AwakeTimeBoolTerm();
            term3.SetWaitLength(3);

            myBoard.SetTerm(term1, 0);
            myBoard.SetTerm(term2, 1);
            myBoard.SetTerm(term3, 2);
        }
    }
    [System.Serializable]
    public class TestBuilderMono_case2 : TestBoardBuilder
    {
        [SerializeField] GameObject[] objects = new GameObject[3];
        [SerializeField] GameObject[] buttons = new GameObject[3];
        public string saveKey;

        protected override TransitionBoard InitBoard()
        {
            return new MonoTranBoard();
        }

        protected override void AddState()
        {
            var factory = new TransitionStateFactory();
            myBoard.AddState((TransitionState)factory.Create());
            myBoard.AddState((TransitionState)factory.Create());
            myBoard.AddState((TransitionState)factory.Create());

            var monoBoard = (MonoTranBoard)myBoard;
            monoBoard.SetObject(0, objects[0]);
            monoBoard.SetObject(1, objects[1]);
            monoBoard.SetObject(2, objects[2]);
        }

        protected override void AddLine()
        {
            var line1 = new UITransitonTermLine(true);
            var line2 = new UITransitonTermLine(true);
            var line3 = new UITransitonTermLine(false);

            myBoard.AddLine(line1);
            myBoard.AddLine(line2);
            myBoard.AddLine(line3);
        }

        protected override void SetLineFromTo()
        {
            myBoard.SetLineFromTo(0, 1, 0);
            myBoard.SetLineFromTo(1, 2, 1);
            myBoard.SetLineFromTo(2, 0, 2);
        }

        protected override void SetTerm()
        {
            var term1 = new UITransitionTerm();
            var term2 = new UITransitionTerm();
            var term3 = new UITransitionTerm();

            myBoard.SetTerm(term1, 0);
            myBoard.SetTerm(term2, 1);
            myBoard.SetTerm(term3, 2);

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
    public class TestBuilderMono_case3 : TestBoardBuilder
    {
        public string saveKey;

        public bool LoadEnable()
        {
            return DataSaver.FullSerializSaver.ExsistFile(saveKey);
        }

        protected override TransitionBoard InitBoard()
        {
            
                var result = DataSaver.FullSerializSaver.LoadAction<MonoTranBoard>(saveKey);
                return result;
            
        }

        protected override void AddState()
        {
        }

        protected override void AddLine()
        {
        }

        protected override void SetLineFromTo()
        {
        }

        protected override void SetTerm()
        {
        }
    }

}
