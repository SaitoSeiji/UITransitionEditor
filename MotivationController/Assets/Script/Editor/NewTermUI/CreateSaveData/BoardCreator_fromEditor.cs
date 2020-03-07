using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aoji_EditorUI
{
    public class BoardCreator_fromEditor
    {
        public Dictionary<int, GameObject> stateList;

        public void SetState(UIBaseGraphView graphView)
        {
            stateList = new Dictionary<int, GameObject>();
            for(int i = 0; i<graphView._nodeList.Count; i++)
            {
                stateList.Add(i, graphView._nodeList[i].obj);
            }
        }

        public void TestSetState()
        {
            foreach(var state in stateList)
            {
                Debug.Log("key:" + state.Key + " | value:" + state.Value.name);
            }
        }
    }
}
