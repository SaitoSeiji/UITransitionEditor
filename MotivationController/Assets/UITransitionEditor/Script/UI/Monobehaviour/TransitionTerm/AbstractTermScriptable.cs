using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aojiru_UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;

public static class TermScriptableCreator
{
    public static void Create<T>(UICanvasBase from,UICanvasBase to)
        where T : AbstractTermScriptable
    {
        string rootDirName = "UITransitionEditor/UITermCondition";
        if (!Directory.Exists(rootDirName))
        {
            Directory.CreateDirectory(rootDirName);
        }
        string dirName = rootDirName + "/" + from.gameObject.name;
        if (!Directory.Exists(dirName))
        {
            Directory.CreateDirectory(dirName);
        }
        string fileName =  dirName+ "/" +to.gameObject.name;
        if (File.Exists(fileName))
        {
            AssetDatabase.DeleteAsset(fileName);
        }
            
        T sctiptableData = ScriptableObject.CreateInstance<T>();
        AssetDatabase.CreateAsset(sctiptableData, "UITransitionEditor/UITermCondition/" + from.gameObject.name);
        EditorUtility.SetDirty(sctiptableData);
        AssetDatabase.SaveAssets();
    }
    
}
#endif

public abstract class AbstractTermScriptable : ScriptableObject
{
    public abstract void AwakeAction();
    public abstract void StartAction();
    public abstract void UpdateAction();


    /// <summary>
    /// メッセージ送信用の関数
    /// </summary>
    public abstract void TranspotMessage_uiActive();
    
}
