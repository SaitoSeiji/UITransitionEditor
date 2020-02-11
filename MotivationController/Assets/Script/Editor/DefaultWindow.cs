using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;

public  class DefaultWindow : EditorWindow
{
    Vector2 _windowSize=new Vector2(1080,960);
    
    protected static void ShowWindow<T>()
        where T : EditorWindow
    {
        EditorWindow.GetWindow<T>();
    }
    protected virtual void OnEnable()
    {
        SetWindowSize(GetWindowSize());
    }
    #region node

    //static public void RemoveNode<T>(NodeSet<T> nodeSet, int index)
    //    where T : NodeData
    //{
    //    nodeSet.RemoveNode(index);
    //    nodeSet.ResetRect();
    //}
    //static public void RemoveNode<T>(NodeSet<T> nodeSet, T data)
    //    where T : NodeData
    //{
    //    nodeSet.RemoveNode(data);
    //    nodeSet.ResetRect();
    //}
    //public static void DrawNode<T>(NodeSet<T> nodeSet,string name,int nuberSet)
    //    where T : NodeData
    //{
    //    nodeSet.DrawNode(name,nuberSet);
    //}
    #endregion
    

    //windowのサイズの設定
    void SetWindowSize(Vector2 targetSize)
    {
        maxSize = targetSize + new Vector2(100, 100);
        minSize = targetSize;
    }

    //windowサイズの登録　子クラスで初期サイズを変えたいときにoverride
    protected virtual Vector2 GetWindowSize()
    {
        return _windowSize;
    }
}

#endif
