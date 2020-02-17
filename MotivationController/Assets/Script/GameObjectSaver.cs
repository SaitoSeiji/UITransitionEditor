using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//生成されるときにInastanceIdHolderを生成する必要がある
//重複しそう
[System.Serializable]
public class GameObjectSaver
{
    [System.NonSerialized] InstanceIdHolder _obj;
    [SerializeField] int _instanceId;
    #region static
    public static List<GameObjectSaver> Covert2List(List<GameObject> objList)
    {
        var result = new List<GameObjectSaver>();
        foreach (var obj in objList)
        {
            var holder=InstanceIdHolder.AddIdHolder(obj);
            result.Add(new GameObjectSaver(holder));
        }
        return result;
    }
    #endregion
    #region constract
    public GameObjectSaver(InstanceIdHolder obj)
    {
        _obj = obj;
        SetInstanceId();
    }
    #endregion

    public GameObject GetObj()
    {
        if (_obj == null) LoadObject();
        return _obj.gameObject;
    }
    public void SetInstanceId()
    {
        _instanceId = _obj.GetInstanceID();
    }

    public void LoadObject()
    {
        _obj = InstanceIdCash.Instance.GetId(_instanceId);
    }
}