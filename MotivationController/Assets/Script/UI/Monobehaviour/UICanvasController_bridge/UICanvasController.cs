using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace aojiru_UI
{
    //UIの開閉などの機能の記述を行う
    //UICanvasが使用する
    public class UICanvasController :AbstractTransitonController<UITransitionTerm<UICanvasBase>, UICanvasBase>
    {
        #region singleTon
        private static UICanvasController instance;
        public static UICanvasController Instance
        {
            get
            {
                if (instance == null)
                {
                    Type t = typeof(UICanvasController);

                    instance = (UICanvasController)FindObjectOfType(t);
                    if (instance == null)
                    {
                        Debug.LogError(t + " をアタッチしているGameObjectはありません");
                    }
                }

                return instance;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            // 他のゲームオブジェクトにアタッチされているか調べる
            // アタッチされている場合は破棄する。
            CheckInstance();
        }
        

        protected bool CheckInstance()
        {
            if (instance == null)
            {
                instance = this as UICanvasController;
                return true;
            }
            else if (Instance == this)
            {
                return true;
            }
            Destroy(this);
            return false;
        }
        #endregion
        //stackなどの機能はこっちに移してもいいかも？

        UICanvasOpenImplementor _uiCvImpl;
        [SerializeField] UICanvasBase nowCanvas;
        [SerializeField] HandInit_UITransition_canvasBase initer;
        [SerializeField] LoadInit_UITran_cv loadInit;
        [SerializeField] string saveFileName;

        protected override void Start()
        {
            base.Start();
            _uiCvImpl = new DefaultCanvasImpl(nowCanvas);
        }

        protected override void Update()
        {
            base.Update();
            nowCanvas = _nowKey;
        }
        protected override void InitTransitionTerm()
        {
            if (loadInit.InitEnable()) _transitionTermList = loadInit.Init();
            else _transitionTermList = initer.Init();
        }


        protected override UICanvasBase SetFirstKey()
        {
            return nowCanvas;
        }

        //uiの遷移処理
        protected override void KeyChengeAction(UITransitionTerm<UICanvasBase> term)
        {
            base.KeyChengeAction(term);
            TransitionCanvas(term.GetTo(), addOpen: term._SelfActive);
        }
        

        void TransitionCanvas(UICanvasBase nextCanvas,bool addOpen)
        {
            if (addOpen)
            {
                AddCanvas(nextCanvas);
            }
            else
            {
                CloseAndAddCanvas(nextCanvas);
            }
        }
        #region canvasのrawな操作
        void AddCanvas(UICanvasBase target)
        {
            SetSortOrder(target, _uiCvImpl.CaluculateNextSortOrder());
            _uiCvImpl.AddCanvas(target);
        }

        void CloseCanvas(UICanvasBase target)
        {
            _uiCvImpl.CloseCanvas(target, lastOpen: false);
        }

        void CloseToNextCanvas(UICanvasBase nextCanvas)
        {
            _uiCvImpl.CloseCanvas(nextCanvas, lastOpen: true);
        }

        //UIのグループを閉じて別のUIを開く　という動きをしたいができない
        void CloseAndAddCanvas(UICanvasBase nextCanvas)
        {
            var impl = (DefaultCanvasImpl)_uiCvImpl;
            if (impl.ContaintHitory(nextCanvas))
            {
                CloseToNextCanvas(nextCanvas);
            }
            else
            {
                CloseCanvas(impl._historyTop);
                AddCanvas(nextCanvas);
            }
        }

        void SetSortOrder(UICanvasBase target, int order)
        {
            target.GetComponent<Canvas>().sortingOrder = order;
        }
        #endregion

        [ContextMenu("save")]
        public void SaveAction()
        {
            var list= initer.Init();
            DataSaver.FullSerializSaver.SaveAction(list, saveFileName);
        }
    }
}
