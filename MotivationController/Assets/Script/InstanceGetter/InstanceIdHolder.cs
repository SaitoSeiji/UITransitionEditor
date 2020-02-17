using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceIdHolder : MonoBehaviour
{
    public static InstanceIdHolder AddIdHolder(GameObject obj)
    {
        var add= obj.AddComponent<InstanceIdHolder>();
        return add;
    }

    public static void RemoveIdHolder(GameObject obj,int id)
    {
        var array=obj.GetComponents<InstanceIdHolder>();
        foreach(var data in array)
        {
            if (data.GetInstanceID() == id)
            {
                Destroy(data);
                return;
            }
        }
    }
    
}
