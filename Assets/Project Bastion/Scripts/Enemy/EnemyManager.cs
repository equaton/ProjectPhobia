using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[Serializable]
public class EnemyManager 
{

	[HideInInspector] public GameObject enemyInstance;				// Reference to the Enemy Instance.
	[HideInInspector] private EnemyHealth enemyHealth;				// Reference to the EnemyHealth script.
	[HideInInspector] private EnemyAttack enemyAttack;				// Reference to the EnemyAttack script.
	[HideInInspector] public NavMeshAgent enemyNavAgent;			// Reference to the Enemy Mesh Navigation Agent.
	[HideInInspector] public Transform enemyTarget;					// Reference to the Target the Enemy will at on. Defaulting to the Player.
	[HideInInspector] public StateController controller;			// Reference to the Enemy State controller script that manages the AI.
	public bool aiActive = true;									// Toggle Enemy AI on and off.

	// Use this for initialization
	public void SetupEnemy ()
	{
		// Get the reference to the Enemy components.
		enemyAttack = enemyInstance.GetComponent <EnemyAttack> ();
		enemyHealth = enemyInstance.GetComponent <EnemyHealth>();
		enemyNavAgent = enemyInstance.GetComponent<NavMeshAgent>();
		enemyTarget = GameObject.FindGameObjectWithTag ("Player").transform;
		controller = enemyInstance.GetComponent<StateController> ();

		// Setup the AI.
		aiActive = true;
		controller.SetupAI(aiActive);
	}
		
}
