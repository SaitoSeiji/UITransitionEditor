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
            CanvasSetState(target, true);
            target.GetComponent<Canvas>().sortingOrder = CaluculateNextSortOrder();
            _openCanvasHirtory.Push(target);
        }

        public override void CloseCanvas(UICanvasBase nextCanvas)
        {
            if (_openCanvasHirtory.Contains(nextCanvas))
            {
                while (true)
                {
                    //nextCanvasより上の階層のものをすべて閉じる
                    //lastOpen=falseならnextCanvasも閉じる
                    var topCanvas = _openCanvasHirtory.Pop();
                    CanvasSetState(topCanvas, false);
                    if (topCanvas == nextCanvas) break;
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
        protected override int CaluculateNextSortOrder()
        {

            if (_openCanvasHirtory.Count==0) return 0;
            var head = _openCanvasHirtory.Peek();
            return head.GetComponent<Canvas>().sortingOrder + 1;
        }

        //履歴stackに含まれているかどうか
        public bool ContaintHitory(UICanvasBase target)
        {
            return _openCanvasHirtory.Contains(target);
        }


        void CanvasSetState(UICanvasBase obj, bool active)
        {
            if (active) obj.ChengeUIState(UICanvasBase.UISTATE.ACTIVE);
            else obj.ChengeUIState(UICanvasBase.UISTATE.CLOSE);
        }
    }
}
