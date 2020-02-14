using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractComponent_uiActiveInterface<T> : AbstractComponent<T>, IMessageTransporter
    where T:AbstractComponentData_uiActiveInterface
{
    protected override void Awake()
    {
        base.Awake();
        SetTransportParent_privete();
    }

    #region interfaceの実装
    public void SetTransportParent_privete()
    {
        var parent = MessageTransporter.FindParentTransporter(transform);
        if (parent != null) parent.SetMessageTarget(this.gameObject);
    }

    public void TranspotMessage_uiActive()
    {
        if (_CompData == null) return;
        _CompData.TranspotMessage_uiActive();
    }
    #endregion
}
