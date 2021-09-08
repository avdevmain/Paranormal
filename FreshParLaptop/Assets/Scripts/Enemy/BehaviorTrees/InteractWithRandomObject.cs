using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class InteractWithRandomObject : Action
{


    public Severity severity;

  public SharedGameObjectList objectsToInteractWith;
  private GameObject objToInteract;
  public SharedTransform targetToThrow;

  public override TaskStatus OnUpdate()
  {

    int index = Random.Range(0, objectsToInteractWith.Value.Count);

    objToInteract = objectsToInteractWith.Value[index];

    if (objToInteract.GetComponent<AudioObject>())
    {
        objToInteract.GetComponent<AudioObject>().RpcUse();
    }
    else if(objToInteract.GetComponent<PhysicalObject>())
    {
        Debug.LogWarning("Добавить звуки для физических предметов");

        int randomX = Random.Range(-1,2);
        int randomY = Random.Range(-1,2);
        int randomZ = Random.Range(0,2);
        float power = 0f;
        if (severity == Severity.easy)
        {
            power = 60f;
            //Уронить
            objToInteract.GetComponent<PhysicalObject>().RpcThrow(new Vector3(1f * randomX,1f * randomY,1f * randomZ) * power);
        }  
        else
        if (severity == Severity.medium)
        {
            power = 90f;
            //Уронить посильнее
            objToInteract.GetComponent<PhysicalObject>().RpcThrow(new Vector3(1f * randomX,1f * randomY,1f * randomZ) * power);

        }
        else
        {
            power = 120f;
            //Кинуть в игрока
            if (targetToThrow.Value == null)
                objToInteract.GetComponent<PhysicalObject>().RpcThrow(new Vector3(1f * randomX,1f * randomY,1f * randomZ) * power);
            else
            {
                Vector3 throwLine = (targetToThrow.Value.position - transform.position).normalized;
                objToInteract.GetComponent<PhysicalObject>().RpcThrow(throwLine * power);
            }
        }
    }
    else if (objToInteract.GetComponent<LightSource>())
    {
       
        if (severity == Severity.easy)
        {
            StartCoroutine("LightFlick");
        }
        else
        {
            LightSource obj = objToInteract.GetComponent<LightSource>();
        if (severity == Severity.medium)
        {
            obj.state = !obj.state;
        }
        else
        {
            obj.interactable = false;
            obj.state = false;
            // + звук разбивания и замена модельки
            Debug.LogWarning("Добавить звук разбивания и смену модельки лампочки");
        }
        }
    }
    else
    if (objToInteract.GetComponent<Door>())
    {
        Door doorObj = objToInteract.GetComponent<Door>();
        if (severity == Severity.easy)
        {
            doorObj.OpenDoor(45f);
        }
        else
        if (severity == Severity.medium)
        {
            doorObj.OpenDoor(85f);
        }
        else
        {
            doorObj.OpenDoor(125f);
        }
    }
    else
    if (objToInteract.GetComponent<Window>())
    {
        Window windowObj = objToInteract.GetComponent<Window>();
        if (severity == Severity.easy)
        {
            windowObj.OpenDoor(45f);
        }
        else
        if (severity == Severity.medium)
        {
            windowObj.OpenDoor(85f);
        }
        else
        {
            windowObj.OpenDoor(125f);
        }
    }
    else{
        Debug.LogError("What the fuck? What object do you have? Or the list is empty? There is " + objectsToInteractWith.Value.Count);
        return TaskStatus.Failure;
    }

      return TaskStatus.Success;
  }

  private IEnumerator LightFlick()
  {
      LightSource obj = objToInteract.GetComponent<LightSource>();
    obj.state = !obj.state;
    yield return new WaitForSecondsRealtime(0.3f);
    obj.state = !obj.state;
  }
}

    public enum Severity
    {
        easy,
        medium,
        hard
    }