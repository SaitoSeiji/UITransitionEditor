using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    //コンクリートブールタームの種類を列挙
    //現状エディタ拡張で使用
    public enum BoolTermType
    {
        AwakeTime,//activeになってからの時間で条件指定
        OnClickTime//指定のボタンをクリックした回数で条件指定
    }

    //UIの遷移条件　bool条件
    [System.Serializable]
    public abstract class AbstractUIBoolTerm : AbstractComponentData_uiActiveInterface
    {
        [SerializeField] bool _isSatisfy;
        public bool _IsSatisfy { get { return _isSatisfy; } private set { _isSatisfy = value; } }

        public override void AwakeAction()
        {
            InitAction();
        }

        public override void StartAction()
        {
        }

        public override void UpdateAction()
        {
            if (ConcreteTerm())
            {
                _IsSatisfy = true;
            }
            else
            {
                _IsSatisfy = false;
            }
        }
        
        protected virtual void InitAction()
        {
        }
        #region interfaceの実装
        //public void SetTransportParent_privete()
        //{
        //    var parent = MessageTransporter.FindParentTransporter(transform);
        //    if (parent != null) parent.SetMessageTarget(this.gameObject);
        //}
        
        #endregion
        //具体的な条件
        //満たされていればTrueなことが名前からわかりずらい
        protected abstract bool ConcreteTerm();

        public abstract BoolTermType GetTermType();

        public static System.Type GetBoolTermType(BoolTermType type)
        {
            switch (type)
            {
                case BoolTermType.AwakeTime:
                    return typeof(AwakeTimeBoolTerm);
                case BoolTermType.OnClickTime:
                    return typeof(OnClickTimeBoolTerm);
                default:
                    return null;
            }
        }
    }
}
