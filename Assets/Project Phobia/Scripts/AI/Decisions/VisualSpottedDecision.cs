using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName= "EnemyAI/Decisions/VisualSpottedDecision")]
public class VisualSpottedDecision : EnemyDecision {

	public override bool Decide (StateController controller)
	{
		bool targerVisible = Look(controller);
		return targerVisible;
	}

	//check is the object can see another with a spherecast
	private bool Look(StateController controller)
	{
		RaycastHit hit;

		Debug.DrawRay (controller.gameObject.transform.position, controller.gameObject.transform.forward.normalized * controller.enemyStats.lookRange, Color.green);

		if (Physics.SphereCast (controller.gameObject.transform.position, controller.enemyStats.lookSpereCastRadius, controller.gameObject.transform.forward, out hit, controller.enemyStats.lookRange)
			&& hit.collider.CompareTag ("Player")) 
		{
			controller.enemyTarget = hit.transform;
			return true;

		} else {
			return false;
		}
	}
}

