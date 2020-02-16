using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AbstractTerm
{

    public abstract void AwakeAction();
    public abstract void StartAction();
    public abstract void UpdateAction();

    /// <summary>
    /// メッセージ送信用の関数
    /// </summary>
    public abstract void TranspotMessage_uiActive();
}


