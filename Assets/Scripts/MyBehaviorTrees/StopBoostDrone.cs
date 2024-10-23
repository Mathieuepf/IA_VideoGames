using System;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

public class StopBoostDrone : Action
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        IsBoostedDrone.StopBoostedDrone();
        return TaskStatus.Success;
    }
}
