using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCanvasImpl : UICanvasImplementor
{
    public DefaultCanvasImpl(UICanvasBase firstCanvas) : base(firstCanvas)
    {

    }

    Stack<UICanvasBase> _openCanvasHirtory = new Stack<UICanvasBase>();

    public override void AddCanvas(UICanvasBase target)
    {
        //ヘッドを見たいだけなのに長い
        if (_openCanvasHirtory.Count>0)
        {
            var head = _openCanvasHirtory.Pop();
            head.ChengeUIState(UICanvasBase.UISTATE.SLEEP);
            _openCanvasHirtory.Push(head);
        }

        target.gameObject.SetActive(true);
        _openCanvasHirtory.Push(target);
        target.ChengeUIState(UICanvasBase.UISTATE.ACTIVE);
    }

    public override void CloseCanvas(UICanvasBase target)
    {
        if (_openCanvasHirtory.Contains(target))
        {
            while (true)
            {
                //閉じたい対象とそれより上の階層のものをすべて閉じる
                //操作できるUIをnextCanvasにする
                var next = _openCanvasHirtory.Pop();
                next.gameObject.SetActive(false);
                next.ChengeUIState(UICanvasBase.UISTATE.CLOSE);
                if (next == target)
                {
                    break;
                }
            }
        }
        else
        {
            //閉じるものがスタックにないときのエラー
            //わかりやすいエラーコードにしたい
            Debug.Log("DefaultCanvasImpl error");
            return;
        }
    }

    public override void CloseToNextCanvas(UICanvasBase nextCanvas)
    {
        if (_openCanvasHirtory.Contains(nextCanvas))
        {
            while (true)
            {
                //閉じたい対象よりそれより上の階層のものをすべて閉じる
                //操作できるUIをnextCanvasにする
                var next = _openCanvasHirtory.Pop();
                if (next == nextCanvas)
                {
                    _openCanvasHirtory.Push(next);
                    next.ChengeUIState(UICanvasBase.UISTATE.ACTIVE);
                    break;
                }
                else
                {
                    next.gameObject.SetActive(false);
                    next.ChengeUIState(UICanvasBase.UISTATE.CLOSE);
                }
            }
        }
        else
        {
            //閉じるものがスタックにないときのエラー
            //わかりやすいエラーコードにしたい
            Debug.Log("DefaultCanvasImpl error");
            return;
        }
    }

    public override int CaluculateNextSortOrder()
    {
        var head = _openCanvasHirtory.Pop();
        int headSortOrder = head.SelfCanvas.sortingOrder;
        _openCanvasHirtory.Push(head);
        return headSortOrder+1;
    }
    
}
