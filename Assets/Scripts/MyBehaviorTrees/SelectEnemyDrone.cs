using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("MyTasks")]
[TaskDescription("Select non targeted enemy Drone")]

public class SelectEnemyDrone : Action
{
	IArmyElement m_ArmyElement;
	public SharedTransform target;
	public SharedFloat minRadius;
	public SharedFloat maxRadius;

	public override void OnAwake()
	{
		m_ArmyElement =(IArmyElement) GetComponent(typeof(IArmyElement));
	}

	public override TaskStatus OnUpdate()
	{
		if (m_ArmyElement.ArmyManager == null) return TaskStatus.Running; // la r�f�rence � l'arm�e n'a pas encore �t� inject�e

		target.Value = m_ArmyElement.ArmyManager.GetNearestEnemy<Drone>(transform.position,minRadius.Value,maxRadius.Value)?.transform;
		if (target.Value != null) return TaskStatus.Success;
		else return TaskStatus.Failure;

	}
}