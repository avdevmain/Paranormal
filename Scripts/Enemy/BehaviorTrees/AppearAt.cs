using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class AppearAt : Action
{
    public SharedVector3 appearPos;
    
    public Severity severity;
    //private NavMeshAgent agent;

    private void Start() {
        //agent = GetComponent<NavMeshAgent>();
    }
    
    public override TaskStatus OnUpdate()
    {
        
        
        GetComponent<Enemy>().AppearAtPos(appearPos.Value,severity);
        if (severity == Severity.easy)
        {

        }
        else
        if (severity == Severity.medium)
        {

        }
        else
        {

        }
        return TaskStatus.Success;
    }
}
