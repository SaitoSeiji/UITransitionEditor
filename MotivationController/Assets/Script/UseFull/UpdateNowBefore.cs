using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateNowBefore<T>
{
    public T beforeValue { get; private set; }
    public T nowValue { get; private set; }

    Trriger swichTrriger=new Trriger();
    public bool SwitchTrriger { get { return swichTrriger._Trriger; } }
    
    public void UpdateNowValue(T value)
    {
        //nowValue=valueの時リターン
        //nowValue==nullの時はEqualsが使えないのでこのように書いた
        if (nowValue == null && value == null) return;
        if (nowValue!=null&&nowValue.Equals(value)) return;

        beforeValue = nowValue;
        nowValue = value;
        swichTrriger._Trriger = true;
    }
    
}
