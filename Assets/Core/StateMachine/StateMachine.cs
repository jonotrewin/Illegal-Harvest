using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected BasicState _currentState;
    public BasicState CurrentState { get { return _currentState; } }


    

    protected void Start()
    {
        _currentState = GetInitialState();

        if(_currentState != null)
        {
            _currentState.Enter();
        }
    }

    protected void Update()
    {
      

        if( _currentState != null )
        {
            _currentState.UpdateLogic();
        }
    }

    private void FixedUpdate()
    {
        if(_currentState != null)
        {
            _currentState.UpdatePhysics();
        }
    }

    public void ChangeStates(BasicState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();

        Debug.Log(this.gameObject.name + " changing to: " + _currentState);
    }

    protected virtual BasicState GetInitialState()
    {
        return null;
    }
}
