using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class GetActionReq : Conditional
{

    public enum ActionType
    {
        none,
        easier,
        harder
    }

    public ActionType actionType;

    public SharedFloat totalActiv;
    public SharedFloat diffModifier;

    public SharedFloat requirement;
    public override TaskStatus OnUpdate()
    {
        float tempValue = 0;
        switch(actionType)
        {
            case ActionType.harder:
                tempValue = 0.4f * totalActiv.Value + 29f + 10f * diffModifier.Value;
                requirement.Value = Mathf.Round(tempValue / 5.0f) * 5;  //Округление до ближайшей пятерки (39 -> 40, 22 -> 20)
            break;
            case ActionType.easier:
                tempValue = 0.3f * totalActiv.Value + 4.7f + 10f * diffModifier.Value;
                requirement.Value = Mathf.Round(tempValue / 5.0f) * 5;
            break;
            default:
                return TaskStatus.Failure;
            
        }

        return TaskStatus.Success;
 
    }
}
