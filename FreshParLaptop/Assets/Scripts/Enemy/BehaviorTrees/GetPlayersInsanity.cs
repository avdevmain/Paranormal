using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("IdleTree")]
public class GetPlayersInsanity : Action
{
  

    public SharedFloat avgInsanity;

 

    public override TaskStatus OnUpdate()
    {
        avgInsanity.Value = 100 -  GetComponent<Enemy>().GetAverageSanity();
        return TaskStatus.Success; 
    }

}
