using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName= "EnemyAI/Decisions/LightChasingDecision")]
public class LightChasingDecision : EnemyDecision {

	private float startLightningVeloocity = 0.1f;
	private float startLightningThreshold = 1f;
	public float damptime = 0.5f;

	public override bool Decide (StateController controller)
	{
		bool targetVisible = SeeLight (controller);
		return targetVisible;

	}
	//check if the objecty can see another with the Spherecast
	private bool SeeLight (StateController controller)
	{
		//Check if the enemy is hit by the Light Aura - it is active around the player when the torchlight is on
		Collider[] lightCollider = Physics.OverlapSphere (controller.gameObject.transform.position, controller.enemyStats.hearingSphereRange, 1 << 13);

		//If it is hit
		if (lightCollider.Length != 0) {
			startLightningThreshold = Mathf.SmoothDamp (startLightningThreshold, 1.1f, ref startLightningVeloocity, damptime);
			Debug.Log (startLightningThreshold);
			if (startLightningThreshold > 0) {
				return true;
			} else {
				return false;
			}
		} else {
			startLightningThreshold = Mathf.SmoothDamp (startLightningThreshold, 0f, ref startLightningVeloocity, damptime);
			Debug.Log (startLightningThreshold);

			if (startLightningThreshold < 0.01) {
				return false;
			} else {
				return true;
			}
		
	
		}
			
	}
}
