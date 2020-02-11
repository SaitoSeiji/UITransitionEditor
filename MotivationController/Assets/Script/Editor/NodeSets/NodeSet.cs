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
    
    protected List<T> _nodeList = new List<T>();
    
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
            Rect newRect = CaliculateRect(GetInitRect(), _arrangeX, i);
            _nodeList[i].SetRect(newRect);
        }
    }
    Rect GetInitRect()
    {
        return new Rect(_firstPos.x, _firstPos.y, _nodeSize.x, _nodeSize.y);
    }

    Rect CaliculateRect(Rect firstPos, bool arrangeX, int nodeCount)
    {
        Rect result = firstPos;
        float nodeInterval = 5;
        int stepCount = nodeCount / _arrangeCount;//段の数
        int pieceCount = nodeCount % _arrangeCount;//個数の位置
        result.x += (_nodeSize.x + nodeInterval) * ((arrangeX) ? pieceCount : stepCount);
        result.y += (_nodeSize.y + nodeInterval) * ((arrangeX) ? stepCount : pieceCount);
        return result;
    }

    #endregion
    public string GetColorCodeString()
    {
        return "flow node " + _colorCode;
    }
    #region node関連

    public void AddNode(T nodeData)
    {
        //ノードのrectの計算
        Rect makeRect = CaliculateRect(GetInitRect(), _arrangeX, _nodeList.Count);
        //ノードの登録
        nodeData.SetRect(makeRect);
        _nodeList.Add(nodeData);
    }

    public void RemoveNode(int index)
    {
        _nodeList.RemoveAt(index);
    }
    public void RemoveNode(T data)
    {
        _nodeList.Remove(data);
    }

    public void DrawNode(string name, int numberSet)
    {
        for (int i = 0; i < _nodeList.Count; i++)
        {
            Rect newRect = GUI.Window(i + numberSet, GetRect(i),_nodeList[i].CallBack, name + i, GetColorCodeString());
            SetRect(i, newRect);
        }
    }
    #endregion
}

public abstract class NodeData
{
    Rect _myRect;

    public NodeData()
    {
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
    }
    public abstract void AbstractCallBack();
    
}