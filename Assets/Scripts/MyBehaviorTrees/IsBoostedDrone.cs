using System;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

public class IsBoostedDrone : Action
{
    private static Boolean _isBoosted;
    public override void OnStart()
    {
        
    }

    public override TaskStatus OnUpdate()
    {
        if (_isBoosted)
        {
            Debug.Log("IsBoostedDrone true");
            return TaskStatus.Failure;
        }
        Debug.Log("IsBoostedDrone false");
        return TaskStatus.Success;
    }
    
    public static void BoostRedDrone()
    {
        _isBoosted = true;
    }

    public static void StopBoostedDrone()
    {
        _isBoosted = false;
    }
}