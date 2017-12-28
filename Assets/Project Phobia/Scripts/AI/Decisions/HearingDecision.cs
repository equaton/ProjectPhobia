using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName= "EnemyAI/Decisions/HearingDecision")]
public class HearingDecision : EnemyDecision {

	public override bool Decide (StateController controller)
	{
		bool targerVisible = Hear(controller);
		return targerVisible;
	}

	//check is the object can see another with a spherecast
	private bool Hear(StateController controller)
	{

		Collider[] soundCollider =  Physics.OverlapSphere (controller.gameObject.transform.position, controller.enemyStats.hearingSphereRange, 1 << 12);


		if (soundCollider.Length != 0) 
		{
			controller.enemyTarget = soundCollider[0].transform;
			return true;

		} else {
			return false;
		}
	}
}
