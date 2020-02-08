using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIの開閉などの機能の記述を行う
//UICanvasが使用する
public class UICanvasController : SingletonMonoBehaviour<UICanvasController>
{
    //stackなどの機能はこっちに移してもいいかも？

    UICanvasOpenImplementor _uiCvImpl;
    [SerializeField] UICanvasBase firstCanvas;

    private void Start()
    {
        _uiCvImpl =new DefaultCanvasImpl(firstCanvas);
    }


    public void AddCanvas(UICanvasBase target)
    {
        SetSortOrder(target, _uiCvImpl.CaluculateNextSortOrder());
        _uiCvImpl.AddCanvas(target);
    }

    public void CloseCanvas(UICanvasBase target)
    {
        _uiCvImpl.CloseCanvas(target,lastOpen:false);
    }

    public void CloseToNextCanvas(UICanvasBase nextCanvas)
    {
        _uiCvImpl.CloseCanvas(nextCanvas,lastOpen:true);
    }

    void SetSortOrder(UICanvasBase target, int order)
    {
        target.SelfCanvas.sortingOrder = order;
    }
}
