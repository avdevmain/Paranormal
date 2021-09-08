using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class GetDiffModifier : Action
{
    public SharedFloat diffModifier;

    public override TaskStatus OnUpdate()
    {
        diffModifier.Value = GetComponent<Enemy>().difficultyMultiplier;
        return TaskStatus.Success; 
    }

}
