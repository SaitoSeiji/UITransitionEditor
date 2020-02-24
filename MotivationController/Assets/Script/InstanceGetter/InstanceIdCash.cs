using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aojiru_UI;
public class InstanceIdCash : SingletonMonoBehaviour<InstanceIdCash>
{
    List<InstanceIdHolder> idList;

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
    
}
