using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WanderingState : enemyState
{
    
    public WanderingState(Enemy enemyChar, enemyStateMachine stateMach) : base(enemyChar, stateMach)
    {
        
    }

    private float stuckTimer = 30f;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Now in state Wandering");


        /*

        //Если !enemyCharacter.canWander то переход в состояние Room

        Vector3 dest = Vector3.zero;
        if (!RandomNavSphere(out dest)) 
        {
            enemyCharacter.ChangeState(enemyCharacter.idle);
            return;
        }
        enemyCharacter.enemyMovement.SetDestination(dest);
        */


        //if (!LevelController.instance.currentGhostRoom.isBasementOrAttic && SoundController.instance.GetFloorTypeFromPosition(vector.y) != LevelController.instance.currentGhostRoom.floorType)
        //При этом странном условии переход в состояние Idle должен идти, а не установка назначения. В чем прикол? В подвале и на чердаке нельзя бродить?

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        /*
        stuckTimer = Mathf.Clamp(stuckTimer-Time.deltaTime, 0, 30f);
        if (stuckTimer<=0)
        {
            enemyCharacter.ChangeState(enemyCharacter.idle);
            return;
        }
        if (enemyCharacter.enemyMovement.IfBadWay())
        {
            enemyCharacter.ChangeState(enemyCharacter.idle);
        }   */
    }

    private bool RandomNavSphere(out Vector3 hitPos) //Случайная точка в радиусе 3
	{
		NavMeshHit navMeshHit;
		if (NavMesh.SamplePosition(Random.insideUnitSphere * 3f + enemyCharacter.transform.position, out navMeshHit, 3f, 1))
		{
			hitPos = navMeshHit.position;
			return true;
		}
		hitPos = Vector3.zero;
		return false;
	}
}
