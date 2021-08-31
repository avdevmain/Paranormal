using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class FloatSum : Action
{
    public SharedFloat var1;
    public SharedFloat var2;
    public SharedFloat var3;
    public SharedFloat var4;

    public SharedFloat result;
  
    public override TaskStatus OnUpdate()
    {
        result.Value  = var1.Value + var2.Value + var3.Value + var4.Value;

        if (result.Value == 0)
            return TaskStatus.Failure;
        else
        return TaskStatus.Success;
    }


    public override void OnReset()
    {
        var1.Value = 0;
        var2.Value = 0;
        var3.Value = 0;
        var4.Value = 0;
        result.Value = 0;
    }
}
