using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIの遷移条件　bool条件
public abstract class AbstractUIBoolTerm : MonoBehaviour
{
    [SerializeField]bool _isSatisfy;
    public bool _IsSatisfy { get { return _isSatisfy; }private set { _isSatisfy = value; } }

    private void Awake()
    {
        InitAction();
    }

    private void OnEnable()
    {
        EnableAction();
    }

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

    protected virtual void EnableAction()
    {

    }

    protected virtual void InitAction()
    {

    }

    //具体的な条件
    //満たされていればTrueなことが名前からわかりずらい
    protected abstract bool ConcreteTerm();
}
