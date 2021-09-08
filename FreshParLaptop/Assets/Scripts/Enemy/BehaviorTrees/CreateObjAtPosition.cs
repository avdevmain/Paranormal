using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class CreateObjAtPosition : Action
{

public GameObject objToSpawn;
public SharedVector3 positionToSpawn;
public AudioClip sound;
public bool withEmf;
    public override TaskStatus OnUpdate()
    {
        if (!objToSpawn)
        return TaskStatus.Failure;

       GetComponent<Enemy>().SpawnObjOnPos(objToSpawn, positionToSpawn.Value, sound, withEmf);
        
        return TaskStatus.Success;


    }


}
