using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateMachine
{
    public enemyState CurrentState {get; private set;}

    public void Initialize(enemyState startState)
    {
        CurrentState = startState;
        startState.Enter();
    }

    public void ChangeState(enemyState newState)
    {
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }
}
