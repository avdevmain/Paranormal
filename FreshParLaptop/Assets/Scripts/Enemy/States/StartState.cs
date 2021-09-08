using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : enemyState
{
   public StartState(Enemy enemyChar, enemyStateMachine stateMach) : base(enemyChar, stateMach)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Now in state Start");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        /*
        if (enemyCharacter.HasTimerReachedZero())
            enemyCharacter.ChangeState(enemyCharacter.idle); */
    }
}
