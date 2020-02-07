using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IUICv_active
{
    //UICanvasBaseのStateがActiveに変更されたときによぶ初期化関数
    //連鎖する性質がある
    //連鎖をすべて書くのはめんどくさい　メッセージを利用できないかね？
    void ActiveInitAction();
}
