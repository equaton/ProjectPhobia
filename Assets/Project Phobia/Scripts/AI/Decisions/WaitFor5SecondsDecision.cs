using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This decisor will wait for 5 seconds, then return negative and modify the enemytarget of the controller to another point at random of the map.

[CreateAssetMenu (menuName= "EnemyAI/Decisions/WaitFor5SecondsDecision")]
public class WaitFor5SecondsDecision : EnemyDecision {

	public override bool Decide (StateController controller)
	{
		MonoBehaviour _mb;
		_mb = GameObject.FindObjectOfType<MonoBehaviour> ();

		Debug.Log ("Starting wait");
		_mb.StartCoroutine( Wait(controller));

		Debug.Log ("Bored now"); 

		Vector3 randomDirection = Random.insideUnitSphere * controller.enemyStats.lookSpereCastRadius;
		randomDirection += controller.gameObject.transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, controller.enemyStats.lookSpereCastRadius, 1);
		Vector3 finalPosition = hit.position;

		controller.navMeshAgent.destination = finalPosition;

		return false;
	}

	//check is the object can see another with a spherecast
	private IEnumerator Wait(StateController controller)
	{
		WaitForSeconds wait = new WaitForSeconds (5);
		yield return wait;
	}


}

