using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomState : enemyState
{
    private float stuckTimer = 30f;
    public RoomState(Enemy enemyChar, enemyStateMachine stateMach) : base(enemyChar, stateMach)
    {
        
    }

    public override void Enter()
    {
        base.Enter();

        /*
        Debug.Log("Now in state Room");

        Vector3 position = Vector3.zero;
        if (GetRandomPosInRoom(out position))
        {
            enemyCharacter.enemyMovement.SetDestination(position);
            return;
        }
        enemyCharacter.ChangeState(enemyCharacter.idle);
        */
      
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        /*
        stuckTimer = Mathf.Clamp(stuckTimer - Time.deltaTime, 0, 30);
        if (stuckTimer<=0)
        {
            enemyCharacter.ChangeState(enemyCharacter.idle);
            return;
        }

        if (enemyCharacter.enemyMovement.IfBadWay())
        {
            enemyCharacter.ChangeState(enemyCharacter.idle);
            return;
        } */

    }

    private bool GetRandomPosInRoom(out Vector3 hitPos)
    {
        float maxDist = Random.Range(0f, 0.5f);
       BoxCollider boxCollider = enemyCharacter.GetRoomCollider(enemyCharacter.favouriteRoom.GetSiblingIndex());
       NavMeshHit navMeshHit;
       if (NavMesh.SamplePosition(new Vector3(Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x), boxCollider.bounds.min.y, Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z)), out navMeshHit, maxDist,1))
		{
			hitPos = navMeshHit.position;
			return true;
		}
		hitPos = Vector3.zero;
		return false;
    }
}
