using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataSaver;
using System.IO;
namespace aojiru_UI
{
    #region initer
    public abstract class AbstractTranDataInit<STATE,LINE,TERM>
        where LINE : AbstractTransitionLine<STATE,TERM>
        where TERM: AbstractTransitionTerm
    {
        public abstract List<LINE> Init();
        public abstract bool InitEnable();
    }

    public abstract class UITransitionTermInit<STATE> : AbstractTranDataInit<STATE, UITransitonTermLine<STATE>, UITransitionTerm>
    {

    }
    #region onclickTransition
    //public abstract class OnclickTranInit<T> : AbstractTranDataInit<OnclickTransition<T>>
    //{
    //    protected List<GameObject> _objList;
    //    public void GetButton(List<GameObject> objList)
    //    {
    //        _objList = objList;
    //    }
    //}


    //public class HandInit_Onclick_int : OnclickTranInit<int>
    //{
    //    public override List<OnclickTransition<int>> Init()
    //    {
    //        var result = new List<OnclickTransition<int>>();
    //        for (int i = 0; i < _objList.Count; i++)
    //        {
    //            var addData = new OnclickTransition<int>(i, i + 1);
    //            addData.AddButton(_objList[i]);
    //            result.Add(addData);
    //            if (i == _objList.Count - 1) addData.SetTo(0);
    //        }
    //        return result;
    //    }

    //    public override bool InitEnable()
    //    {
    //        return true;
    //    }
    //}

    //public class LoadInit_OnClick<T> : OnclickTranInit<T>
    //{
    //    string _path;

    //    public LoadInit_OnClick(string path)
    //    {
    //        _path = path;
    //    }

    //    public override List<OnclickTransition<T>> Init()
    //    {
    //        var loder = new JsonTranDataSaver<OnclickTransition<T>>();
    //        var result = loder.LoadAction(_path);

    //        return result;
    //    }

    //    public override bool InitEnable()
    //    {
    //        return FullSerializSaver.ExsistFile(_path);
    //    }
    //}

    #endregion
    #region UITransitionInit
    [System.Serializable]
    public class HandInit_UITransition_canvasBase : UITransitionTermInit<UICanvasBase>
    {
        [SerializeField] UICanvasBase[] from;
        [SerializeField] UICanvasBase[] to;
        [SerializeField] GameObject[] click;

        public override List<UITransitonTermLine<UICanvasBase>> Init()
        {
            var list = new List<UITransitonTermLine<UICanvasBase>>();
            for(int i = 0; i < from.Length; i++)
            {
                var trriger = new OncliclUITrrigerTerm();
                trriger.AddButton(click[i]);
                var term = new UITransitionTerm();
                term.SetTrriger(trriger);
                var add = new UITransitonTermLine<UICanvasBase>(i==0);
                add.SetFrom(from[i]);
                add.SetTo(to[i]);
                add.SetTerm(term);
                list.Add(add);
            }

            return list;
        }

        public override bool InitEnable()
        {
            return true;
        }
    }
    [System.Serializable]
    public class LoadInit_UITran_cv: UITransitionTermInit<UICanvasBase>
    {
        [SerializeField] string key;
        public override List<UITransitonTermLine<UICanvasBase>> Init()
        {
            return FullSerializSaver.LoadAction<List<UITransitonTermLine<UICanvasBase>>>(key);
        }

        public override bool InitEnable()
        {
            return FullSerializSaver.ExsistFile(key);
        }
    }
    #endregion
    #endregion
}