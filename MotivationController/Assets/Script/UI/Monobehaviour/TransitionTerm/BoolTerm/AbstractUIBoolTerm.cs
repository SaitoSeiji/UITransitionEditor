using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIの遷移条件　bool条件
public abstract class AbstractUIBoolTerm : MonoBehaviour,IUICv_active
{
    [SerializeField]bool _isSatisfy;
    public bool _IsSatisfy { get { return _isSatisfy; }private set { _isSatisfy = value; } }

    private void Awake()
    {
        InitAction();
    }

    //private void OnEnable()
    //{
    //    ActiveInitAction();
    //}

    private void Update()
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

    //対象のUICanvasのstateがActiveになったら呼ばれる初期化関数
    public virtual void ActiveInitAction()
    {

    }

    protected virtual void InitAction()
    {

    }

    //具体的な条件
    //満たされていればTrueなことが名前からわかりずらい
    protected abstract bool ConcreteTerm();
}
