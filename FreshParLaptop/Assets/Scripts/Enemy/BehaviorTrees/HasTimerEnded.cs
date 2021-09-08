using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class HasTimerEnded : Conditional
{
    public override TaskStatus OnUpdate()
    {
        if (GetComponent<Enemy>().HasTimerReachedZero())
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}
