using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//データとコンポーネントの動機をとる
public abstract class Data2CompConverter
{

    //データをもとにコンポーネントを作成
    public static List<T> SyncData2Comps<T,K>(GameObject compObj,List<K> dataList)
        where T:AbstractComponent<K>
        where K:AbstractComponentData
    {
        var beforeComps = compObj.GetComponents<T>();
        foreach(var destroyComp in beforeComps)
        {
            MonoBehaviour.DestroyImmediate(destroyComp);
        }

        var resultList = new List<T>();
        foreach(var addData in dataList)
        {
            var addComp= compObj.AddComponent<T>();
            addComp.SetData((K)addData);
            resultList.Add(addComp);
        }
        return resultList;
    }
    public static T SyncData2Comp<T, K>(GameObject compObj,K data)
        where T : AbstractComponent<K>
        where K : AbstractComponentData
    {
        var tempDataList = new List<K>();
        tempDataList.Add(data);
        var list = SyncData2Comps<T, K>(compObj, tempDataList);
        if (list.Count == 0) return null;
        else return list[0];
    }

    //コンポーネントをもとにデータを作成
    public static List<K> SyncComp2Datas<T, K>(GameObject compObj)
        where T : AbstractComponent<K>
        where K : AbstractComponentData
    {
        var nowComps = compObj.GetComponents<T>();
        var _newDataList = new List<K>();
        foreach (var nowComp in nowComps)
        {
            _newDataList.Add(nowComp.GetData());
        }
        return _newDataList;
    }
    public static K SyncComp2Data<T, K>(GameObject compObj)
       where T : AbstractComponent<K>
       where K : AbstractComponentData
    {
        var list=SyncComp2Datas<T, K>(compObj);
        if (list.Count == 0) return null;
        else return list[0];
    }
}
