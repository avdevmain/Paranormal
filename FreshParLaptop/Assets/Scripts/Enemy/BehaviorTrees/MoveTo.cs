using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class MoveTo : Action
{
public SharedGameObject target;
public SharedVector3 targetPosition;

private NavMeshAgent navMeshAgent;

private EnemyMovement enemyMotor;


    public override void OnAwake()
    {
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyMotor = GetComponent<Enemy>().enemyMovement;
    }
    public override TaskStatus OnUpdate()
    {
        if (HasArrived()) {
            return TaskStatus.Success;
        }

        enemyMotor.SetDestination(Target());

        return TaskStatus.Running;
    }


    private Vector3 Target()
    {
        if (target.Value != null) {
            return target.Value.transform.position;
        }
        return targetPosition.Value;
    }


    private  bool HasArrived()
    {     
        float remainingDistance;
        if (navMeshAgent.pathPending) 
        {
            remainingDistance = float.PositiveInfinity;
        } 
        else 
        {
            remainingDistance = navMeshAgent.remainingDistance;
        }
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
    }

    public override void OnReset()
    {
        base.OnReset();
        target = null;
        targetPosition = Vector3.zero;
    }
}
