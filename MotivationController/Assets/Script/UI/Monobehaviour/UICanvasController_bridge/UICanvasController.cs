using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIの開閉などの機能の記述を行う
//UICanvasが使用する
public class UICanvasController : MonoBehaviour
{
    //stackなどの機能はこっちに移してもいいかも？

    UICanvasImplementor _uiCvImpl;
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
        _uiCvImpl.CloseCanvas(target);
    }

    public void CloseToNextCanvas(UICanvasBase nextCanvas)
    {
        _uiCvImpl.CloseToNextCanvas(nextCanvas);
    }

    void SetSortOrder(UICanvasBase target, int order)
    {
        target.SelfCanvas.sortingOrder = order;
    }
}
