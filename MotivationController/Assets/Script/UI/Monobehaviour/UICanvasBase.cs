using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    //UIの1ページを担当するCanvasにつける 及びそれを表す
    public class UICanvasBase : MonoBehaviour
    {
        [System.NonSerialized]UICanvasController _uiCtrl;
        


        public enum UISTATE
        {
            ACTIVE,//active=trueで入力を受け付ける
            SLEEP,//active=trueだが入力を受け付けない
            CLOSE//active=false
        }
        [SerializeField] UISTATE _nowUIState = UISTATE.CLOSE;

        [SerializeField] bool canInput = false;
        bool CanInput
        {
            get
            {
                bool result = true;
                if (_nowUIState != UISTATE.ACTIVE) result = false;
                else if (_isActiveWait.WaitNow) result = false;

                canInput = result;
                return result;
            }
        }
        #region canInputを構成する条件軍
        //入力を受け付けない待ち時間
        [System.NonSerialized] TimeFlag _isActiveWait = new TimeFlag();
        #endregion

        private void Awake()
        {
            _uiCtrl = UICanvasController.Instance;
        }

        public void ChengeUIState(UISTATE nextState)
        {
            if (nextState == UISTATE.ACTIVE && _nowUIState != UISTATE.ACTIVE)
            {
                ActiveInitAction();
            }
            _nowUIState = nextState;
        }

        public void ActiveInitAction()
        {
            _isActiveWait.StartWait(0.5f);
            //SendMessage2Target();
        }
    }
}
