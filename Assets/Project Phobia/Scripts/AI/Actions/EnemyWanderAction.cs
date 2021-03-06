﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (menuName = "EnemyAI/Action/Wander")]

public class EnemyWanderAction : EnemyAction
{

	public override void Act (StateController controller)
	{
		Wander (controller);
	}
    
	private void Wander (StateController controller)
	{

		float distance = controller.navMeshAgent.remainingDistance;

		//controller.navMeshAgent.destination = GetRandomLocation ();

		if ( distance != Mathf.Infinity && controller.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && controller.navMeshAgent.remainingDistance == 0)             
			controller.navMeshAgent.destination = GetRandomLocation ();
		


		//controller.navMeshAgent.destination = controller.randompositionherearoundtheenemy
		controller.navMeshAgent.isStopped = false;

		//when destination reached, geherate another random position for the Enemy to navigate to.
		//need to generate a random point in the allowable navmesh only!
	}

	private Vector3 GetRandomLocation ()
	{

		NavMeshTriangulation navMeshData =	NavMesh.CalculateTriangulation ();

		// Pick the first indice of a random triangle in the nav mesh
		int t = Random.Range (0, navMeshData.indices.Length - 3);

		// Select a random point on it
		Vector3 point = Vector3.Lerp (navMeshData.vertices [navMeshData.indices [t]], navMeshData.vertices [navMeshData.indices [t + 1]], Random.value);
		Vector3.Lerp (point, navMeshData.vertices [navMeshData.indices [t + 2]], Random.value);

		return point;
	}
} 
	