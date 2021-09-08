using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class GetEnemyActivity : Action
{
    public SharedFloat totalActivity;


    public override TaskStatus OnUpdate()
    {
        Enemy enemy = GetComponent<Enemy>();
        totalActivity.Value = enemy.activityMultiplier + enemy.bonusActivity;
        return TaskStatus.Success; 
    }

}
