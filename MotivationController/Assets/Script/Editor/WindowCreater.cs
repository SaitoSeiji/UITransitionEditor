using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class WindowCreater : EditorWindow
{
    Vector2 _windowSize=new Vector2(1080,960);

    [MenuItem("Custom/SampleWindow")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow<WindowCreater>();
    }
    protected virtual void OnEnable()
    {
        SetWindowSize(_windowSize);
    }
    void WindowCallback(int id)
    {
        //GUI.DragWindow();
    }
    #region node
    protected void AddNode(NodeSet nodeSet,int addCount)
    {
        for (int i = 0; i < addCount; i++)
        {
            AddNode(nodeSet);
        }
    }

    protected void AddNode(NodeSet nodeSet)
    {
        nodeSet.AddRect();
    }

    protected void RemoveNode(NodeSet nodeSet,int index)
    {
        nodeSet.RemoveRect(index);
        nodeSet.ResetRect();
    }
    protected void DrawNode(NodeSet nodeSet,string name,int nuberSet)
    {
        for (int i = 0; i < nodeSet._rectCount; i++)
        {
            Rect newRect = GUI.Window(i+nuberSet, nodeSet.GetRect(i), WindowCallback, name + i, "flow node 5");
            nodeSet.SetRect(i,newRect);
        }
    }
    #endregion


    //windowのサイズの設定
    void SetWindowSize(Vector2 targetSize)
    {
        maxSize = targetSize + new Vector2(100, 100);
        minSize = targetSize;
    }

    
}

public class NodeSet
{
    Vector2 _firstPos;
    Vector2 _nodeSize;
    bool _arrangeX = true;//trueならx方向に並べる
    int _arrangeCount = 5;//並べる最大個数

    //int _nodeCount;

    List<Rect> _rectList = new List<Rect>();
    public int _rectCount { get { return _rectList.Count; } }

    public NodeSet(Vector2 firstPos, Vector2 nodeSize)
    {
        _firstPos = firstPos;
        _nodeSize = nodeSize;
        //_nodeCount = 0;
        _arrangeX = true;
        _arrangeCount = 5;
    }
    public NodeSet(Vector2 firstPos, Vector2 nodeSize
        , bool arrangeX, int arrangeCount)
    {
        _firstPos = firstPos;
        _nodeSize = nodeSize;
        //_nodeCount = 0;
        _arrangeX = arrangeX;
        _arrangeCount = arrangeCount;
    }

    public void AddRect()
    {
        Rect result = new Rect(_firstPos.x, _firstPos.y, _nodeSize.x, _nodeSize.y);
        result = CaliculatePositionRect(result, _arrangeX,_rectList.Count);
        _rectList.Add(result);
    }

    public void RemoveRect(int index)
    {
        _rectList.RemoveAt(index);
    }

    public Rect GetRect(int index)
    {
        return _rectList[index];
    }

    public void SetRect(int index,Rect rect)
    {
        _rectList[index] = rect;
    }

    public void ResetRect()
    {
        for(int i = 0; i < _rectList.Count; i++)
        {
            _rectList[i]= new Rect(_firstPos.x, _firstPos.y, _nodeSize.x, _nodeSize.y);
            _rectList[i] = CaliculatePositionRect(_rectList[i], _arrangeX, i);
        }
    }

    Rect CaliculatePositionRect(Rect firstPos,bool arrangeX,int nodeCount)
    {
        Rect result=firstPos;
        float nodeInterval = 5;
        int stepCount = nodeCount / _arrangeCount;//段の数
        int pieceCount = nodeCount % _arrangeCount;//個数の位置
        result.x += (_nodeSize.x + nodeInterval) * ((arrangeX) ? pieceCount : stepCount);
        result.y += (_nodeSize.y + nodeInterval) * ((arrangeX) ? stepCount : pieceCount);
        return result;
    }
}
#endif
