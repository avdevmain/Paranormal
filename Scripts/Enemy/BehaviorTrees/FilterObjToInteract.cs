using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class FilterObjToInteract : Action
{
    public SharedGameObjectList objectsToInteractWith;

    public enum ObjectsToPick
    {
        none,
        sound,
        physics,
        light,
        door,
        window
    }

    public ObjectsToPick filter;

    public override TaskStatus OnUpdate()
    {

        switch (filter)
        {
            case ObjectsToPick.sound:
                foreach (GameObject obj in objectsToInteractWith.Value)
                {
                    if (!obj.GetComponent<AudioObject>())
                        objectsToInteractWith.Value.Remove(obj);
                }
            break;
            case ObjectsToPick.physics:
                foreach (GameObject obj in objectsToInteractWith.Value)
                {
                    if (!obj.GetComponent<PhysicalObject>())
                        objectsToInteractWith.Value.Remove(obj);
                }
            break;
            case ObjectsToPick.light:
            
                foreach (GameObject obj in objectsToInteractWith.Value)
                {
                    if (!obj.GetComponent<LightSource>())
                        objectsToInteractWith.Value.Remove(obj);
                }

            break;

            case ObjectsToPick.door:
                foreach (GameObject obj in objectsToInteractWith.Value)
                {
                    if (!obj.GetComponent<Door>())
                        objectsToInteractWith.Value.Remove(obj);
                }
            Debug.LogWarning("Скрипта для дверей не существеует, как и самих дверей");
            break;
            case ObjectsToPick.window:
                foreach (GameObject obj in objectsToInteractWith.Value)
                {
                    if (!obj.GetComponent<Window>())
                        objectsToInteractWith.Value.Remove(obj);
                }
            Debug.LogWarning("Скрипта для окон не существеует, как и самих дверей");
            break;
            default:
            Debug.LogWarning("Не задано значение фильтра интерактивных предметов ( " + filter + " )");
            return TaskStatus.Failure;
        }

        return TaskStatus.Success;
    }
}
