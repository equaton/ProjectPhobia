using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "EnemyAI/Decisions/LightningDeciins")]
public class LightDecision : EnemyDecision {

	private float startLightningVeloocity = 0.1f;
	private float startLightningThreshold = 0f;

	public override bool Decide (StateController controller)
	{
		bool targetVisible = SeeLight (controller);
		return targetVisible;

	}
	//check if the objecty can see another with the Spherecast
	private bool SeeLight(StateController controller)
	{
		//Check if the enemt is hit by the light collider in the lightAI layer
		Collider[] lightCollider = Physics.OverlapSphere(controller.gameObject.transform.position, controller.enemyStats.hearingSphereRange, 1<<13);

		//If it is hit
		if (lightCollider.Length != 0) {
			startLightningThreshold = Mathf.SmoothDamp (startLightningThreshold, 1.1f, ref startLightningVeloocity, 1f);

			if (startLightningThreshold > 1) {
				controller.enemyTarget = lightCollider [0].transform;
				Debug.Log("I see you!");
				return true;
			} else {
				return false;
			}
		} else 
		{
			startLightningThreshold = Mathf.SmoothDamp (startLightningThreshold, 0f, ref startLightningVeloocity, 1f);
			return false;
		}
	}
}



