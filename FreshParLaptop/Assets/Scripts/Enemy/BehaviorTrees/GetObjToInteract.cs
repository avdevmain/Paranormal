using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class GetObjToInteract : Action
{
 

    public SharedTransform searchCenter;

    public float searchMinRadius = 0f;
    public float searchMaxRadius = 10f;

    public SharedGameObjectList objectsToInteractWith;
    public override TaskStatus OnUpdate()
    {
        LayerMask mask = LayerMask.GetMask("Interactable");
        
        if (!searchCenter.Value)
            searchCenter.Value = transform;

        Collider[] hitColliders = Physics.OverlapSphere(searchCenter.Value.position, searchMaxRadius, mask);

        foreach (var hitCollider in hitColliders)
        {
            if (Vector3.Distance(hitCollider.transform.position, searchCenter.Value.position) >= searchMinRadius)
                objectsToInteractWith.Value.Add(hitCollider.gameObject);
        }

        return TaskStatus.Success;
    }

}
