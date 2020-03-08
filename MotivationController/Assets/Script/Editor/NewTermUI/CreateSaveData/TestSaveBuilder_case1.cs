using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aojiru_UI;
namespace aoji_EditorUI
{
    public class TestSaveBuilder_case1 : TestBoardBuilder
    {
        SaveBuildePrepare creator;
        public TestSaveBuilder_case1(SaveBuildePrepare c) : base()
        {
            creator = c;
        }

        protected override TransitionBoard InitBoard()
        {
            return new MonoTranBoard();
        }
        protected override void AddState()
        {
            var factory = new TransitionStateFactory();
            var monoBoard = (MonoTranBoard)myBoard;
            for (int i = 0; i < creator.stateList.Count; i++)
            {
                myBoard.AddState((TransitionState)factory.Create());

                monoBoard.SetObject(i, creator.stateList[i]);
            }
        }

        protected override void AddLine()
        {
            for(int i = 0; i < creator.lineConnectList.Count; i++)
            {
                var line = new UITransitonTermLine(creator.lineConnectList[i].active);
                myBoard.AddLine(line);
            }
        }

        protected override void SetLineFromTo()
        {
            for(int i = 0; i < creator.lineConnectList.Count; i++)
            {
                myBoard.SetLineFromTo(
                    from:creator.lineConnectList[i].from
                    ,to:creator.lineConnectList[i].to
                    , line:i);
            }
        }

        protected override void SetTerm()
        {
            for(int i = 0; i < creator.linesTermData.Count; i++)
            {
                myBoard.SetTerm(creator.linesTermData[i].term,creator.linesTermData[i].line);
            }
        }
    }
}
