using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Seek the target specified using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}SeekIcon.png")]
    public class Seek : NavMeshMovement
    {
        [Tooltip("The GameObject that the agent is seeking")]
        public SharedGameObject target;
        [Tooltip("If target is null then use the target position")]
        public SharedVector3 targetPosition;

        private Animator animator;

        public override void OnStart()
        {
            base.OnStart();

            animator = GetComponent<Animator>();
            SetDestination(Target());
        }

        // Seek the destination. Return success once the agent has reached the destination.
        // Return running if the agent hasn't reached the destination yet
        public override TaskStatus OnUpdate()
        {
            if (HasArrived()) {
                StopAnimation();
                return TaskStatus.Success;
            }
            SetDestination(Target());
            SyncAnimation();
            return TaskStatus.Running;
        }
        
        // Return targetPosition if target is null
        private Vector3 Target()
        {
            if (target.Value != null) {
                return target.Value.transform.position;
            }
            return targetPosition.Value;
        }

        private void SyncAnimation()
        {
            Vector3 localDir = transform.InverseTransformDirection(navMeshAgent.velocity);
         
         if (localDir.x>0.1f)
            animator.SetFloat("DirX", localDir.x);
         if (localDir.z>0.1f)
            animator.SetFloat("DirZ", localDir.z);
         

            //transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
        }
        private void StopAnimation()
        {
            animator.SetFloat("DirX", 0);
            animator.SetFloat("DirZ", 0);
        }

        public override void OnReset()
        {
            base.OnReset();
            target = null;
            targetPosition = Vector3.zero;
        }
    }
}