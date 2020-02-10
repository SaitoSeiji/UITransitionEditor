using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    //void WindowCallback(int id)
    //{
    //    //GUI.DragWindow();
    //    EditorGUILayout.LabelField("ああ", "ああ");
    //}
    #region node
    protected void AddNode<T>(NodeSet<T> nodeSet,List<T> data)
        where T : NodeData
    {
        for (int i = 0; i < data.Count; i++)
        {
            AddNode(nodeSet,data[i]);
        }
    }

    protected void AddNode<T>(NodeSet<T> nodeSet,T data)
        where T: NodeData
    {
        nodeSet.AddNode(data);
    }

    protected void RemoveNode<T>(NodeSet<T> nodeSet,int index)
        where T : NodeData
    {
        nodeSet.RemoveNode(index);
        nodeSet.ResetRect();
    }
    protected void DrawNode<T>(NodeSet<T> nodeSet,string name,int nuberSet)
        where T : NodeData
    {
        for (int i = 0; i < nodeSet._rectCount; i++)
        {
            Rect newRect = GUI.Window(i+nuberSet, nodeSet.GetRect(i), nodeSet._nodeList[i].CallBack, name + i, nodeSet.GetColorCodeString());
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

#endif
