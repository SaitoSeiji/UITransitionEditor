using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aojiru_UI;
public class InstanceIdCash : SingletonMonoBehaviour<InstanceIdCash>
{
    [SerializeField] bool _awakeIdCash;
    List<InstanceIdHolder> idList;
    List<UICanvasBase> canvasList;

    void IdCash()
    {
        var data =Resources.FindObjectsOfTypeAll(typeof(InstanceIdHolder));
        idList = new List<InstanceIdHolder>();
        foreach(var d in data)
        {
            idList.Add((InstanceIdHolder)d);
        }
    }

    public InstanceIdHolder GetId(int id)
    {
        if (idList == null) IdCash();

        foreach(var data in idList)
        {
            if (id == data.GetInstanceID())
            {
                return data;
            }
        }
        return null;
    }

    void CanvasCash()
    {
        var data = Resources.FindObjectsOfTypeAll(typeof(UICanvasBase));
        canvasList = new List<UICanvasBase>();
        foreach (var d in data)
        {
            canvasList.Add((UICanvasBase)d);
        }
    }

    public UICanvasBase GetCanvas(int id)
    {
        if (canvasList == null) CanvasCash();

        foreach (var data in canvasList)
        {
            if (id == data.GetInstanceID())
            {
                return data;
            }
        }
        return null;
    }
}
