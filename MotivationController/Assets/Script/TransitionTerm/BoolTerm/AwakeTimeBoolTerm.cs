using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    //アクティブになってからの経過時間が条件
    //指定時間を待たずに条件を満たすことがある
    public class AwakeTimeBoolTerm<T> : AbstractUIBoolTerm<T>
    {
        //editorで使用するためにpublic　ほんとはパブリックにしたくない
        [SerializeField]float waitLength;

        [System.NonSerialized] TimeFlag flag;
        protected override void EnableAction()
        {
            base.EnableAction();
            flag.StartWait(waitLength);
        }

        protected override bool ConcreteTerm()
        {
            if (flag == null) flag.StartWait(waitLength);
            return !flag.WaitNow;
        }
    }
}
