using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : enemyState
{
    float idleTimer;
    private float huntMultiplier;
    private int maxRandomAbilityValue = 100;

    private float typeMultiplier;

    public IdleState(Enemy enemyChar, enemyStateMachine stateMach) : base(enemyChar, stateMach)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Now in state Idle");
        /*

        idleTimer = Random.Range(2f,6f);

        switch(enemyCharacter.GetDiffLevel())
        {
            case LevelManager.DiffLevel.easy:
            maxRandomAbilityValue = 100;
            break;
            case LevelManager.DiffLevel.normal:
            maxRandomAbilityValue = 115;
            break;
            case LevelManager.DiffLevel.hard:
            maxRandomAbilityValue = 130;
            break;
            default:
            break;
        }

        if (enemyCharacter.enemyTraits.enemyType == EnemyTraits.EnemyType.Oni && enemyCharacter.ArePlayersHere())
            typeMultiplier  = 30;
        
        if (enemyCharacter.enemyTraits.enemyType == EnemyTraits.EnemyType.Wraith && enemyCharacter.hasWalkedInSalt)
            typeMultiplier = 50;
        
        //Если ghostType == Mare, выключен свет в комнате с ним, то huntingMultiplier +=10, если включен, то -=10, если света там в принципе нет, то тоже +10 (фонарики не учитываются)
        
        if (!enemyCharacter.WasDoorOpened()) //Если главная дверь НЕ была открыта, то перейти в фазу Room
            enemyCharacter.ChangeState(enemyCharacter.room);


        if (enemyCharacter.enemyTraits.enemyType == EnemyTraits.EnemyType.Demon)
                huntMultiplier+=15; */
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    { /*
        base.LogicUpdate();
        
        idleTimer = Mathf.Clamp(idleTimer-Time.deltaTime, 0f, 6f);
        if (idleTimer <= 0 )
        {
            //Check what to do next: hunting, wander or favouriteRoom
            float avgInsanity = 100 - enemyCharacter.GetAverageSanity(); 

            
            if (Random.Range(0,2) == 1 && enemyCharacter.canHunt && enemyCharacter.canAttack) 
            {
                if (avgInsanity + huntMultiplier >= 50 && avgInsanity + huntMultiplier < 75 && Random.Range(0,5)==1)
                {
                    enemyCharacter.ChangeState(enemyCharacter.hunt);
                    return;                   
                }
                else
                if (avgInsanity + huntMultiplier >= 75 && Random.Range(0,3) == 1)
                {
                    enemyCharacter.ChangeState(enemyCharacter.hunt);
                    return;
                }
            }

            float value = Mathf.Clamp(avgInsanity + typeMultiplier + enemyCharacter.activityMultiplier, 0, 100); //  if alone + 15
            
            if (Random.Range(0, maxRandomAbilityValue) <= value && Random.Range(0,2) == 1)
            {
                int value1 = Random.Range(0,11);
                if (value1 < 5)
                {
                    enemyCharacter.enemyActivity.Interact();
                    return;
                }
                else
                if (value1 < 9)
                {
                    enemyCharacter.enemyActivity.GhostAbility();
                    return;
                }
                if (Random.Range(0,3) == 1)
                {
                    enemyCharacter.ChangeState(enemyCharacter.wandering);
                    return;
                }
                enemyCharacter.ChangeState(enemyCharacter.room);
            }
            else
            {
                if (Random.Range(0,5) == 1)
                {
                    enemyCharacter.ChangeState(enemyCharacter.wandering);
                    return;
                }
                if (Random.Range(0,4) == 1)
                {
                    enemyCharacter.enemyActivity.Interact();
                    return;
                }
                enemyCharacter.ChangeState(enemyCharacter.room);
            }





        } */
    }
}
