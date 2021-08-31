using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class SetInt : Action
{   
    public SharedInt variable;

    public int targetValue;
    public override TaskStatus OnUpdate()
    {
        variable.Value = targetValue;
        return TaskStatus.Success;
    }
}
