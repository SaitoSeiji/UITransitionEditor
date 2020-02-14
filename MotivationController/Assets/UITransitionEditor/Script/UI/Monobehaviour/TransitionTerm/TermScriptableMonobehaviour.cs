using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScriptableType
{
    Null,
    Trriger,
    Bool
}

public class TermScriptableMonobehaviour : MonoBehaviour,IMessageTransporter
{
    [SerializeField] AbstractTermScriptable _myData;
    [SerializeField] ScriptableType _scriptableType = ScriptableType.Null;
    public ScriptableType _ScriptableType { get { return _scriptableType; } }

    public AbstractTermScriptable GetData()
    {
        return _myData;
    }

    public void SetData(AbstractTermScriptable data,ScriptableType setType)
    {
        _myData = data;
        _scriptableType = setType;
    }
    

    private void Awake()
    {
        SetTransportParent_privete();
        _myData.AwakeAction();
    }

    private void Start()
    {
        _myData.StartAction();
    }

    private void Update()
    {
        _myData.UpdateAction();
    }


    /// <summary>
    /// メッセージ送信用の関数
    /// </summary>
    public void TranspotMessage_uiActive()
    {
        _myData.TranspotMessage_uiActive();
    }
    /// <summary>
    /// メッセージを送ってもらうための登録をする
    /// 外部参照はされない
    /// </summary>
    public void SetTransportParent_privete()
    {
        var parent=MessageTransporter.FindParentTransporter(transform);
        parent.SetMessageTarget(gameObject);
    }
}
