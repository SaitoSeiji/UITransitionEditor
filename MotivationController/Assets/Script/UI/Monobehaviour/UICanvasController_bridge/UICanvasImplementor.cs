using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャンバスの開閉動作の実装を行う
//デザインパターン:ブリッジのImplementorを表現
public abstract class UICanvasImplementor
{
    //コンストラクタなくてもいいかも
    public UICanvasImplementor(UICanvasBase firstCanvas)
    {
        AddCanvas(firstCanvas);
    }

    //現在のキャンバスを閉じて別のものを開く　ができない

    //キャンバスを開く処理
    public abstract void AddCanvas(UICanvasBase target);

    //キャンバスを閉じる処理 いらない？
    public abstract void CloseCanvas(UICanvasBase target);

    //履歴にあるキャンバスまでキャンバスを閉じる処理
    public abstract void CloseToNextCanvas(UICanvasBase nextCanvas);

    //次のsortOrderを計算
    public abstract int CaluculateNextSortOrder();
}
