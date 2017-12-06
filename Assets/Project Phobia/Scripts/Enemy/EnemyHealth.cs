using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float startingHealth = 20;		// The starting health of the enemy.
	public bool isEnemyAlive = true;		// Is the enemy alive or dead?
	public AudioClip enemyHitAudio;			// Clip to play when enemy is hit.
	public AudioClip enemyDeathAudio;		// Clip to play when enemy is dead.
	public EnemyManager enemyManager;		// Reference to the enemy Manager.

	private Rigidbody enemyRigidBody;		// Reference to the Enemy Game Object.
	private float currentHealth;			// The current health of the enemy.
	private ParticleSystem hitParticles;	// Particles to show when the enemy is hit. 
	private AudioSource enemyAudio;			// Reference to the Audio Source of the Enemy.
	private Collider enemyCollider;			// Reference to the enemy Collider
	private EnemyAttack enemyAttack;		// Reference to the enemy attack Script.
	private bool isSinking = false;			// Is the enemy starting to sink after death?
	private float sinkSpeed = 2f;			// Speed of enemy sinking after death
	private float deathTime = 2f;			// Time after the enemy is destroyed after death.
	private Animator animator;				// Reference to the enemy animator.

	// Use this for initialization
	void Awake () {
		
		// Set the current health as the starting health. 
		currentHealth = startingHealth;

		// Get the set of references.
		hitParticles = GetComponentInChildren <ParticleSystem>();
		enemyRigidBody = GetComponent<Rigidbody>();
		enemyAttack = GetComponent<EnemyAttack>();
		enemyAudio = GetComponentInChildren<AudioSource> ();
		enemyCollider = GetComponentInChildren <Collider> ();
		animator = GetComponent<Animator> ();

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

		// Check if the Enemy is dead.
		if (currentHealth < 0 || currentHealth == 0) {
			// Perform a set of actions that occurr on enemy death
			EnemyDeath ();
		} 
		// otherwise play the dit sound and move on.
		else 
		
		{
			// Set AudioSource clip to EnemyHit and play
			enemyAudio.clip = enemyHitAudio;
			enemyAudio.Play ();

			// Play is hit animation.
			animator.SetTrigger("isHit");
		}

	}

	void EnemyDeath()
	{
		// Enemy is dead!
		isEnemyAlive = false;

		// Start the enemy sinking
		StartCoroutine (StartSinking());

		// Set AudioSource clip to EnemyDeath and play
		enemyAudio.clip = enemyDeathAudio;
		enemyAudio.Play ();

		// Disable Enemy movement and AI.
		enemyManager.DisableEnemy();

		// Play death animation.
		animator.SetBool("isAlive", false);
	}

	IEnumerator StartSinking()
	{
		// Make the Enemy RigidBody Kinematic so can be controlled by script.
		enemyRigidBody.isKinematic = true;

		yield return new WaitForSeconds (2);

		// The enemy should sink now in Update().
		isSinking = true;

		// Destroy the Enemy after a couple od seconds for cleanup.
		Destroy (gameObject, deathTime);
	}

}
