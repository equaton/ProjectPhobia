using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float startingHealth = 20;		// The starting health of the enemy.
	public bool isEnemyAlive = true;				// Is the enemy alive or dead?
	public AudioClip enemyHitAudio;			// Clip to play when enemy is hit.
	public AudioClip enemyDeathAudio;		// Clip to play when enemy is dead.

	private Rigidbody EnemyRigidBody;		// Reference to the Enemy Game Object.
	private float currentHealth;			// The current health of the enemy.
	private ParticleSystem hitParticles;	// Particles to show when the enemy is hit. 
	private AudioSource playerAudio;			// Reference to the Audio Source of the Enemy.
	private Collider playerCollider;			// Reference to the enemy Collider
	private bool isSinking = false;			// Is the enemy starting to sink after death?
	private float sinkSpeed = 2f;			// Speed of enemy sinking after death
	private float deathTime = 2f;			// Time after the enemy is destroyed after death.

	// Use this for initialization
	void Awake () {
		
		// Set the current health as the starting health. 
		currentHealth = startingHealth;

		// Get the set of references.
		hitParticles = GetComponentInChildren <ParticleSystem>();
		EnemyRigidBody = GetComponent<Rigidbody>();
		playerAudio = GetComponentInChildren<AudioSource> ();
		playerCollider = GetComponentInChildren <Collider> ();

 	}
	
	// Update 	is called once per frame
	void Update () {
		if (isSinking) 
		{
			// Make the enemy sink.
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);

			// Make the enemy reduce in size.
			transform.localScale -= new Vector3 (0.01f, 0.01f, 0.01f);
		}

	}


	public void TakeDamage (float damage, Vector3 shootPoint)
	{
		// Is the enemy already dead? If yes don't do anything!
		if (!isEnemyAlive) 
		{
			return;
		}


		// Set the position of the particle system to where the Enemy is hit.
		hitParticles.transform.position = shootPoint;

		// Play the particle system
		hitParticles.Play();

		// Remove health based on damage of bullet;
		currentHealth -= damage; 


		// Check if the Eneme is dead.
		if (currentHealth < 0 || currentHealth == 0) {
			// Perform a set of actions that occurr on enemy death
			EnemyDeath ();
		} 
		// otherwise play the dit sound and move on.
		else 
		
		{
			// Set AudioSource clip to EnemyHit and play
			playerAudio.clip = enemyHitAudio;
			playerAudio.Play ();
		}

	}

	void EnemyDeath()
	{
		// Enemy is dead!
		isEnemyAlive = false;

		// Make the collider a trigger so the shot won't hit the enemy anymore
		playerCollider.isTrigger = true;

		// Start the enemy sinking
		StartCoroutine (StartSinking());

		// Set AudioSource clip to EnemyDeath and play
		playerAudio.clip = enemyDeathAudio;
		playerAudio.Play ();
	}

	IEnumerator StartSinking()
	{
		// Make the Enemy RigidBody Kinematic so can be controlled by script.
		EnemyRigidBody.isKinematic = true;

		yield return new WaitForSeconds (2);

		// The enemy should sink now in Update().
		isSinking = true;

		// Destroy the Enemy after a couple od seconds for cleanup.
		Destroy (gameObject, deathTime);
	}

}
