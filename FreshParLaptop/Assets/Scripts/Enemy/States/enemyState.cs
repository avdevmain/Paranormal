using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class enemyState
{

    protected Enemy enemyCharacter;
    protected enemyStateMachine stateMachine;

    protected enemyState(Enemy enemyChar, enemyStateMachine stateMach)
    {
        this.enemyCharacter = enemyChar;
        this.stateMachine = stateMach;
    }

    public virtual void Enter() {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void Exit()
    {

    }


}
