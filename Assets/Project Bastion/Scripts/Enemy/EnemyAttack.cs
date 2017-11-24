using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public float enemyAttackDamage = 20;			// Amount of damage done by Enemy.
	public float enemyTimeBetweenAttacks = 1f;		// Amount of time between one attack and another.
	public bool enemyAttackEnabled = true;			// Is the Enemy attack enabled?

	private GameObject 	player;						// Reference to the Player GameObject.
	private PlayerHealth playerHealth;				// Reference to the Player Health.
	private bool contactWithPlayer;					// Is the Enemy touching the player?
	private float timerForAttacks = 0;					// Timer to calculate when an attack can occurr.

	// Use this for initialization
	void Start () 
	{
		// Get a reference to the Player GameObject.
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent <PlayerHealth>();
	}

	// Update is called once per frame
	void Update () {

		// Increase the timer per the frame time.
		timerForAttacks += Time.deltaTime;

		// Is the timer higher than the time when the Enemy is allowed to attack?
		if (timerForAttacks > enemyTimeBetweenAttacks) 
		{
			//Is the enemy allowed to attack?
			if (enemyAttackEnabled)
			{

				// Is the Enemy touching the Player?
				if (contactWithPlayer) {

					//Is the player still alive?
					if (playerHealth.isPlayerAlive) {
					
						// Inflict damage to the player.
						playerHealth.TakeDamage (enemyAttackDamage);

						// Reset the timer for attacks.
						timerForAttacks = 0;

					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		//is the Collider hit the Player's?
		if (other.gameObject.tag == "Player") 
		{
			// Let the script know that the Enemy is touching the player.
			contactWithPlayer = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		//is the Collider that is being left the Player's?
		if (other.gameObject.tag == "Player") 
		{
			// Let the script know that the Enemy is not touching the player anymore.
			contactWithPlayer = false;
		}
	}

}
