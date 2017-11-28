using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[Serializable]
public class EnemyManager 
{

	[HideInInspector] public GameObject enemyInstance;				// Reference to the Enemy Instance.
	[HideInInspector] public NavMeshAgent enemyNavAgent;			// Reference to the Enemy Mesh Navigation Agent.
	[HideInInspector] public Transform enemyTarget;					// Reference to the Target the Enemy will at on. Defaulting to the Player.
	[HideInInspector] public StateController controller;			// Reference to the Enemy State controller script that manages the AI.
	public bool aiActive = true;									// Toggle Enemy AI on and off.

	[HideInInspector] private EnemyHealth enemyHealth;				// Reference to the EnemyHealth script.
	[HideInInspector] private EnemyAttack enemyAttack;				// Reference to the EnemyAttack script.
	[HideInInspector] private Collider enemyCollider;				// Reference to the enemy Collider.

	// Use this for initialization
	public void SetupEnemy ()
	{
		// Get the reference to the Enemy components.
		enemyAttack = enemyInstance.GetComponent <EnemyAttack> ();
		enemyNavAgent = enemyInstance.GetComponent<NavMeshAgent>();
		enemyTarget = GameObject.FindGameObjectWithTag ("Player").transform;
		controller = enemyInstance.GetComponent<StateController> ();
		enemyHealth = enemyInstance.GetComponent<EnemyHealth> ();
		enemyCollider = enemyInstance.GetComponent<Collider> ();

		// Pass on the reference of this enemyManager to the EnemyHealth script of the EnemyInstance.
		enemyHealth.enemyManager = this;

		// Setup the AI.
		aiActive = true;
		controller.SetupAI(aiActive);
	}
		

	public void DisableEnemy()
	{
		// Make the collider a trigger so the shot won't hit the enemy anymore
		enemyCollider.isTrigger = true;

		// Disable the Enemy attack.
		enemyAttack.enemyAttackEnabled = false;

		//Disable the ai.
		aiActive = false;
		controller.SetupAI (aiActive);
	}
}
