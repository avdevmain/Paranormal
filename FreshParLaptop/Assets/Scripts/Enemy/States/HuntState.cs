using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : enemyState
{
    public HuntState(Enemy enemyChar, enemyStateMachine stateMach) : base(enemyChar, stateMach)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Now in state Hunt");

      
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }
}
