using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("IdleTree")]
public class CanWander : Conditional
{
    public override TaskStatus OnUpdate()
    {
        if (GetComponent<Enemy>().canWander)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }

}
