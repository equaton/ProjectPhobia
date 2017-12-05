using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "EnemyAI/Action/Chase")]

public class EnemyChaseAction : EnemyAction 
{
	public override void Act(StateController controller)
	{
		Chase(controller);
	}

	private void Chase (StateController controller)
	{
		controller.navMeshAgent.destination = controller.enemyTarget.position;
		controller.navMeshAgent.isStopped = false ;
	}

}