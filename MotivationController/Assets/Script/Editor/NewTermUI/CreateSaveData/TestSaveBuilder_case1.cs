using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aojiru_UI;
namespace aoji_EditorUI
{
    public class TestSaveBuilder_case1 : TestBoardBuilder
    {
        BoardCreator_fromEditor creator;
        public TestSaveBuilder_case1(BoardCreator_fromEditor c) : base()
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

        }

        protected override void SetLineFromTo()
        {
            throw new System.NotImplementedException();
        }

        protected override void SetTerm()
        {
            throw new System.NotImplementedException();
        }
    }
}
