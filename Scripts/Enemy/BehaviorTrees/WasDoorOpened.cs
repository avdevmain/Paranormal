using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class WasDoorOpened : Conditional
{
   private Enemy enemyCharacter;

   public override void OnAwake()
   {
       enemyCharacter = GetComponent<Enemy>();
   }
   public override TaskStatus OnUpdate()
   {
        if (enemyCharacter.WasDoorOpened())
            return TaskStatus.Success;
        else
            return TaskStatus.Running;
   }
}
