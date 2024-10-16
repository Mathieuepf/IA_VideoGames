using System;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

public class IsBoostedDrone : Action
{
    public static Boolean isBoosted;
    public override void OnStart()
    {
		
    }

    public override TaskStatus OnUpdate()
    {
        if (isBoosted)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }
    
    public void BoostRedDrone()
    {
        isBoosted = true;
    }
}