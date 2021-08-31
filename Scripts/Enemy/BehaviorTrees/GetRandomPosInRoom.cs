using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class GetRandomPosInRoom : Action
{
    public SharedVector3 destination;

    private Enemy enemy;
    public bool inFavourite;

    public int roomIndex;
    public override TaskStatus OnUpdate()
    {

        enemy = GetComponent<Enemy>();

        destination.Value = Vector3.zero;
        if (inFavourite)
        {
            if (!GetRandomPosInRoomByIndex(enemy.favouriteRoom.GetSiblingIndex())) 
            {
                return TaskStatus.Failure;
            }
        }
        else
        {
           if (!GetRandomPosInRoomByIndex(roomIndex)) 
            {
                return TaskStatus.Failure;
            }
        }
        return TaskStatus.Success;
    }  
    


   private bool GetRandomPosInRoomByIndex(int index)
    {
        destination.Value = Vector3.zero;
       
        float maxDist = Random.Range(0f, 0.5f);
       BoxCollider boxCollider = enemy.GetRoomCollider(index);
       NavMeshHit navMeshHit;
       if (NavMesh.SamplePosition(new Vector3(Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x), boxCollider.bounds.min.y, Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z)), out navMeshHit, maxDist,1))
		{
			destination.Value = navMeshHit.position;
			return true;
		}
		destination.Value = Vector3.zero;
		return false;
    }
}
