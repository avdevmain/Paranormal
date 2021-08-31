using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class GetScareSpot : Action
{
    public SharedTransform searchCenter;
    
    public float searchMinRadius = 0f;
    public float searchMaxRadius = 10f;

    private List<Transform> spotsFound;
    public SharedVector3 scareSpot;
    
    public override TaskStatus OnUpdate()
    {
        spotsFound = new List<Transform>();
        LayerMask mask = LayerMask.GetMask("ScareSpot");
        
        if (!searchCenter.Value)
            searchCenter.Value = transform;

        Collider[] hitColliders = Physics.OverlapSphere(searchCenter.Value.position, searchMaxRadius, mask);

        foreach (var hitCollider in hitColliders)
        {
            if (Vector3.Distance(hitCollider.transform.position, searchCenter.Value.position) >= searchMinRadius)
                spotsFound.Add(hitCollider.transform);
        }

        for (int i =0; i<spotsFound.Count;i++)
        {
            if (IsSeenByTarget(spotsFound[i]))
            {
                NavMeshHit navMeshHit;
                if (NavMesh.SamplePosition(spotsFound[i].position, out navMeshHit, 1f,1))
                {
                    scareSpot.Value = navMeshHit.position;
                }        
            }
        }
        if (scareSpot.Value == Vector3.zero)
        {
            scareSpot.Value = spotsFound[Random.Range(0, spotsFound.Count)].position;
        }
        return TaskStatus.Failure;
    }

    //Точка может быть увидена (не загорожена стеной)
    //Точка не находится в прямой видимости цели  
    //If seeable: use Camera.WorldToViewportPoint, check if X and Y are within 0..1 range AND check Z>0
//(if Z is not greater than 0 it means the point is behind the camera.)

    public bool IsSeenByTarget(Transform objTransform)
    {
        //searchCenter.Value.GetComponent<Interaction>().playerCam.WorldToViewportPoint(point);
        Vector3 viewPos;
        Camera playerCamera = searchCenter.Value.GetComponent<Interaction>().playerCam;
        viewPos = playerCamera.WorldToViewportPoint(objTransform.position);



        if (viewPos.x >=0 && viewPos.x <=1 && viewPos.y >=0 && viewPos.y<=1 && viewPos.z >=0)
            {
                // + проверка рейкастом

                Ray ray = playerCamera.ScreenPointToRay(objTransform.position - playerCamera.transform.position);
                RaycastHit hit;
                 int layerMask = ((1 << 11) | (1 << 12));
            
                if (Physics.Raycast(ray.origin, ray.direction, out hit, 15f, layerMask))
                {
                if (hit.collider.transform == objTransform)
                    {
                    Debug.Log("Вижу");
                    return true;
                    }
                }
            }
        return false;

    }
}
