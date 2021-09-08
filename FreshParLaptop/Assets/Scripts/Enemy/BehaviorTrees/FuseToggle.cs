using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class FuseToggle : Action
{
    private FuseBox fuseBox;
    void Start() {
        fuseBox = Object.FindObjectOfType<FuseBox>();
    }
    public override TaskStatus OnUpdate()
    {
        fuseBox.state = !fuseBox.state;

        return TaskStatus.Success;
    }

}