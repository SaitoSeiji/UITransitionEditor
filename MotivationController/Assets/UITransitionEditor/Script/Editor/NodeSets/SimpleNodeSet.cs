using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace aojiru_UI
{
    public class SimpleNodeSet : NodeSet<SimpleNodeData>
    {
        public SimpleNodeSet(UITermWIndow parentWindow,Vector2 firstPos, Vector2 nodeSize, int colorCode = 0)
            : base(parentWindow,firstPos, nodeSize, colorCode)
        {
        }
        public SimpleNodeSet(UITermWIndow parentWindow, Vector2 firstPos, Vector2 nodeSize
            , bool arrangeX, int arrangeCount, int colorCode = 0)
            : base(parentWindow,firstPos, nodeSize, arrangeX, arrangeCount, colorCode)
        {
        }

        public override void AddNode()
        {
        }
    }

    public class SimpleNodeData : NodeData
    {
        public override void AbstractCallBack()
        {
        }
    }
}