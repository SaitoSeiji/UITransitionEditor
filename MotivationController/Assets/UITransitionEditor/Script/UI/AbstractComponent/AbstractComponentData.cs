using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AbstractComponentData
{
    public abstract void AwakeAction();
    public abstract void StartAction();
    public abstract void UpdateAction();

    
}
