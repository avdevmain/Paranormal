using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
   private Enemy enemy;

   private NavMeshAgent agent;

   private void Start() {

   enemy = GetComponent<Enemy>();    
   agent = GetComponent<NavMeshAgent>();

   //agent.updateRotation = false;

   //agent.Warp(enemy.favouriteRoom.position);

   }
   /*
   void LateUpdate() {


      if (!agent.isOnNavMesh) return;

      if (agent.velocity.sqrMagnitude >Mathf.Epsilon)
      {

         Vector3 localDir = transform.InverseTransformDirection(agent.velocity);
         
         if (localDir.x>0.1f)
            enemy.animator.SetFloat("DirX", localDir.x);
         if (localDir.z>0.1f)
            enemy.animator.SetFloat("DirZ", localDir.z);
         

            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
      }
 

   } */

   
   public void SetDestination(Vector3 dest)
   {
      agent.destination = dest;
   }

   public void WarpTo(Vector3 pos)
   {
      agent.Warp(pos);
   }
 
   public bool IfBadWay()
   {
      if (agent.pathStatus == NavMeshPathStatus.PathPartial || agent.pathStatus == NavMeshPathStatus.PathInvalid || !agent.hasPath)
		{
			return true;
		}
		//if (Vector3.Distance(transform.position, agent.destination) < 1f)
		//{
			//return true;
		//}

      return false;
   }
 
}
