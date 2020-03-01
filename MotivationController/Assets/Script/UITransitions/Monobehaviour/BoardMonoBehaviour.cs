using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aojiru_UI;

public abstract class BoardMonoBehaviour : MonoBehaviour
{
    protected TransitionBoard _tranBoard;
    protected TransitionPin _tranPin;

    virtual protected void Start()
    {
        _tranBoard=InitBoard();
        _tranPin = _tranBoard.CreatePin();
    }

    virtual protected void Update()
    {
        _tranPin.StateAction();
    }

    protected abstract TransitionBoard InitBoard();
    
}
