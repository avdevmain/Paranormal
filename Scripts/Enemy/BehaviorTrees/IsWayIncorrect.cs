using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsWayIncorrect : Conditional
{
    public override TaskStatus OnUpdate()
    {
       if  (GetComponent<Enemy>().enemyMovement.IfBadWay())
        return TaskStatus.Success;
       else
        return TaskStatus.Failure;   
    }
}
