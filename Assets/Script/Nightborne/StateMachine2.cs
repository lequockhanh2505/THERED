using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine2
{
    public IState currentState;
    public void ChangeState(IState newState)
    {
        if (currentState != newState && currentState != null)
        {
            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEnter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }
}
