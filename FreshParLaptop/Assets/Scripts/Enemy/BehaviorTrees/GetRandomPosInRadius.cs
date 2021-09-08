using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class GetRandomPosInRadius : Action
{
    public float minRadius;
    public float radius;
    public SharedTransform target;
    public SharedVector3 destination;

    public override TaskStatus OnUpdate()
    {
        destination.Value = Vector3.zero;
        if (!RandomNavSphere()) 
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }  
    

    private bool RandomNavSphere() //Случайная точка в радиусе 
	{
		NavMeshHit navMeshHit;

            //if (NavMesh.SamplePosition(Random.insideUnitSphere+target.Value, out navMeshHit, radius, 1))
            if (NavMesh.SamplePosition(RandomBetweenRadius3D(), out navMeshHit, radius, 1))
            {
                
                destination.Value = navMeshHit.position;
                return true;
            }
        


		destination.Value = Vector3.zero;
		return false;
	}

    Vector3 RandomBetweenRadius3D()
    {
    if (target.Value == null)
    {
        target.Value = transform;
    }
    float diff = radius - minRadius;
    Vector3 point = Vector3.zero;
    while(point == Vector3.zero)
    {
        point = Random.insideUnitSphere+target.Value.position;
    }
    point = point.normalized * (Random.value * diff + minRadius);
    return point;  
}
}
