using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    public class DefaultCanvasImpl : UICanvasOpenImplementor
    {
        public DefaultCanvasImpl(UICanvasBase firstCanvas) : base(firstCanvas)
        {

        }

        Stack<UICanvasBase> _openCanvasHirtory = new Stack<UICanvasBase>();
        public UICanvasBase _historyTop { get { return _openCanvasHirtory.Peek(); } }

        public override void AddCanvas(UICanvasBase target)
        {
            if (_openCanvasHirtory.Count > 0)
            {
                var head = _openCanvasHirtory.Peek();
                head.ChengeUIState(UICanvasBase.UISTATE.SLEEP);
            }
            CanvasSetActive(target, true);
            _openCanvasHirtory.Push(target);
        }

        public override void CloseCanvas(UICanvasBase nextCanvas, bool lastOpen)
        {
            if (_openCanvasHirtory.Contains(nextCanvas))
            {
                while (true)
                {
                    //nextCanvasより上の階層のものをすべて閉じる
                    //lastOpen=falseならnextCanvasも閉じる
                    var topCanvas = _openCanvasHirtory.Peek();
                    if (topCanvas != nextCanvas)
                    {
                        _openCanvasHirtory.Pop();
                        CanvasSetActive(topCanvas, false);
                    }
                    else if (topCanvas == nextCanvas && !lastOpen)
                    {
                        _openCanvasHirtory.Pop();
                        CanvasSetActive(topCanvas, false);
                        break;
                    }
                    else if (topCanvas == nextCanvas && lastOpen)
                    {
                        CanvasSetActive(topCanvas, true);
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
        public override int CaluculateNextSortOrder()
        {
            var head = _openCanvasHirtory.Peek();
            return head.GetComponent<Canvas>().sortingOrder + 1;
        }

        //履歴stackに含まれているかどうか
        public bool ContaintHitory(UICanvasBase target)
        {
            return _openCanvasHirtory.Contains(target);
        }


        void CanvasSetActive(UICanvasBase obj,bool active)
        {
            obj.gameObject.SetActive(active);
            if (active) obj.ChengeUIState(UICanvasBase.UISTATE.ACTIVE);
            else obj.ChengeUIState(UICanvasBase.UISTATE.CLOSE);
        }
    }
}
