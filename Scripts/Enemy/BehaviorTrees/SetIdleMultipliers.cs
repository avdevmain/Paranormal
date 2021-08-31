using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("IdleTree")]
public class SetIdleMultipliers : Action
{

    public SharedGameObject enemyObject;
    private Enemy enemyCharacter;
    public SharedFloat huntMultiplier;
    public SharedFloat typeMultiplier;
    public SharedFloat maxRandomAbilityValue = 100;
    
    public override void OnAwake()
   {
       Debug.Log(enemyObject.Value.name);
       enemyCharacter = enemyObject.Value.GetComponent<Enemy>();
   }
    public override TaskStatus OnUpdate()
    {
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
            return TaskStatus.Failure;
        }


        if (enemyCharacter.enemyTraits.enemyType == EnemyTraits.EnemyType.Oni && enemyCharacter.ArePlayersHere())
            typeMultiplier  = 30;
        
        if (enemyCharacter.enemyTraits.enemyType == EnemyTraits.EnemyType.Wraith && enemyCharacter.hasWalkedInSalt)
            typeMultiplier = 50;
        
        //Если ghostType == Mare, выключен свет в комнате с ним, то huntingMultiplier +=10, если включен, то -=10, если света там в принципе нет, то тоже +10 (фонарики не учитываются)
        
        if (enemyCharacter.enemyTraits.enemyType == EnemyTraits.EnemyType.Demon)
            huntMultiplier.Value+=15;

        return TaskStatus.Success;
    }
}
