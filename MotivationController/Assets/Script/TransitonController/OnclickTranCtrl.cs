using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//namespace aojiru_UI
//{
//    public class OnclickTranCtrl : AbstractTransitonController<OnclickTransition<int>, int>
//    {
//        [SerializeField] int nowKey;
//        [SerializeField] List<GameObject> btList;


//        OnclickTranInit<int> initer;

//        protected override void InitTransitionTerm()
//        {
//            initer = new LoadInit_OnClick<int>("testSaveBt");
//            if (initer.InitEnable())
//            {
//                _transitionTermList = initer.Init();

//            }
//            else
//            {
//                initer = new HandInit_Onclick_int();
//                initer.GetButton(btList);
//                _transitionTermList = initer.Init();
//            }

//        }

//        protected override int SetFirstKey()
//        {
//            return nowKey;
//        }


//        protected override void Update()
//        {
//            base.Update();
//            nowKey = _nowKey;
//        }


//        [ContextMenu("save")]
//        public void SaveData()
//        {

//            initer = new HandInit_Onclick_int();
//            initer.GetButton(btList);
//            _transitionTermList = initer.Init();

//            var saver = new DataSaver.JsonTranDataSaver<OnclickTransition<int>>();
//            saver.SaveAction(_transitionTermList, "testSaveBt");
//        }
//    }
//}
