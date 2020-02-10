using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class NodeSet<T>
    where T : NodeData
{
    Vector2 _firstPos;
    Vector2 _nodeSize;
    bool _arrangeX = true;//trueならx方向に並べる
    int _arrangeCount = 5;//並べる最大個数
    int _colorCode=0;
    
    //List<Rect> _rectList = new List<Rect>();
    public int _rectCount { get { return _nodeList.Count; } }

    public List<T> _nodeList = new List<T>();

    //描画時に呼ばれる関数
    //ノード内の描画などを担当
    UnityEvent drawCallback = new UnityEvent();
    #region コンストラクタ
    public NodeSet(Vector2 firstPos, Vector2 nodeSize,int colorCode=0)
    {
        _firstPos = firstPos;
        _nodeSize = nodeSize;
        _arrangeX = true;
        _arrangeCount = 5;
        _colorCode = colorCode;
    }
    public NodeSet(Vector2 firstPos, Vector2 nodeSize
        , bool arrangeX, int arrangeCount,int colorCode=0)
    {
        _firstPos = firstPos;
        _nodeSize = nodeSize;
        _arrangeX = arrangeX;
        _arrangeCount = arrangeCount;
        _colorCode = colorCode;
    }
    #endregion
    #region rect関係

    public void AddNode(T nodeData)
    {
        Rect result = new Rect(_firstPos.x, _firstPos.y, _nodeSize.x, _nodeSize.y);
        result = CaliculatePositionRect(result, _arrangeX, _nodeList.Count);
        nodeData.SetRect(result);
        nodeData.SetDefaultCallback(drawCallback);
        _nodeList.Add(nodeData);
    }

    public void RemoveNode(int index)
    {
        _nodeList.RemoveAt(index);
    }

    //public void AddRect()
    //{
    //    Rect result = new Rect(_firstPos.x, _firstPos.y, _nodeSize.x, _nodeSize.y);
    //    result = CaliculatePositionRect(result, _arrangeX, _nodeList.Count);
    //    _rectList.Add(result);
    //}

    //public void RemoveRect(int index)
    //{
    //    _rectList.RemoveAt(index);
    //}

    public Rect GetRect(int index)
    {
        return _nodeList[index].GetRect();
    }

    public void SetRect(int index, Rect rect)
    {
        //_rectList[index] = rect;
        _nodeList[index].SetRect(rect);
    }

    public void ResetRect()
    {
        for (int i = 0; i < _nodeList.Count; i++)
        {
            //_rectList[i] = new Rect(_firstPos.x, _firstPos.y, _nodeSize.x, _nodeSize.y);
            //_rectList[i] = CaliculatePositionRect(_rectList[i], _arrangeX, i);
            var newRect= new Rect(_firstPos.x, _firstPos.y, _nodeSize.x, _nodeSize.y);
            newRect = CaliculatePositionRect(newRect, _arrangeX, i);
            _nodeList[i].SetRect(newRect);
        }
    }
    #endregion
    Rect CaliculatePositionRect(Rect firstPos, bool arrangeX, int nodeCount)
    {
        Rect result = firstPos;
        float nodeInterval = 5;
        int stepCount = nodeCount / _arrangeCount;//段の数
        int pieceCount = nodeCount % _arrangeCount;//個数の位置
        result.x += (_nodeSize.x + nodeInterval) * ((arrangeX) ? pieceCount : stepCount);
        result.y += (_nodeSize.y + nodeInterval) * ((arrangeX) ? stepCount : pieceCount);
        return result;
    }

    public string GetColorCodeString()
    {
        return "flow node " + _colorCode;
    }

    #region callback関係

    public void AddCallback(UnityAction ua)
    {
        drawCallback.AddListener(ua);
    }

    public void ResetCallback(UnityAction ua)
    {
        drawCallback = new UnityEvent();
    }

    #endregion
}

public abstract class NodeData
{
    UnityEvent _defaultCallback;
    Rect _myRect;

    public NodeData()
    {
    }

    public void SetDefaultCallback(UnityEvent callback)
    {
        _defaultCallback = callback;
    }

    public void SetRect(Rect rect)
    {
        _myRect = rect;
    }
    public Rect GetRect()
    {
        return _myRect;
    }

    public void CallBack(int id)
    {
        AbstractCallBack();
        _defaultCallback.Invoke();
    }
    public abstract void AbstractCallBack();
    
}