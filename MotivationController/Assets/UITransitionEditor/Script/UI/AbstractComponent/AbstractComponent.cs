using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractComponent<T> : MonoBehaviour
    where T:AbstractComponentData
{
    [SerializeField] T _compData;
    public T _CompData { get { return _compData; } }

    public void SetData(T data)
    {
        _compData = data;
    }

    public T GetData()
    {
        return _compData;
    }

    protected virtual void Awake()
    {
        if (_compData != null)
        {
            _compData.AwakeAction();
        }
    }

    private void Start()
    {
        if (_compData != null)
        {
            _compData.StartAction();
        }
    }

    private void Update()
    {
        if (_compData != null)
        {
            _compData.AwakeAction();
        }
    }
}
