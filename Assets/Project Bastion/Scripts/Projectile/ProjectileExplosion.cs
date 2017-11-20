using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{
	public LayerMask m_EnemyMask;                        // Used to filter what the explosion affects, this should be set to "Enemy".
	public ParticleSystem m_ExplosionParticles;         // Reference to the particles that will play on explosion.
	public AudioSource m_ExplosionAudio;                // Reference to the audio that will play on explosion.
	public float m_MaxDamage = 100f;                    // The amount of damage done
	public float m_ExplosionForce = 1000f;              // The amount of force added to a enemy at the centre of the explosion.
	public float m_MaxLifeTime = 2f;                    // The time in seconds before the projectile is removed.
	public float m_ExplosionRadius = 0.1f;                // The maximum distance away from the explosion enemies can be and are still affected.


	private void OnTriggerEnter (Collider other)
	{
		// Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
		//Collider[] colliders = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_EnemyMask);

		Debug.Log ("bang!");

			// Find the EnemyHeatlh script associated with the rigidbody.
			//EnemyHealth targetHealth = targetRigidbody.GetComponent<EnemyHealth> ();

			// If there is no TankHealth script attached to the gameobject, go on to the next collider.
			//if (!EnemyHealth)
			//	continue;

			// Calculate the amount of damage the target should take based on it's distance from the shell.
			//float damage = CalculateDamage (targetRigidbody.position);

			// Deal this damage to the tank.
			//targetHealth.TakeDamage (damage);
		}

		// Unparent the particles from the shell.
		//m_ExplosionParticles.transform.parent = null;

		// Play the particle system.
	//	m_ExplosionParticles.Play();

		// Play the explosion sound effect.
		//m_ExplosionAudio.Play();

		// Once the particles have finished, destroy the gameobject they are on.
		//ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
		//Destroy (m_ExplosionParticles.gameObject, mainModule.duration);

		// Destroy the shell.
		//Destroy (gameObject);
	}

/*
	private float CalculateDamage (Vector3 targetPosition)
	{
		// Create a vector from the shell to the target.
		Vector3 explosionToTarget = targetPosition - transform.position;

		// Calculate the distance from the shell to the target.
		float explosionDistance = explosionToTarget.magnitude;

		// Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
		float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

		// Calculate damage as this proportion of the maximum possible damage.
		float damage = relativeDistance * m_MaxDamage;

		// Make sure that the minimum damage is always 0.
		damage = Mathf.Max (0f, damage);

		return damage;
	} */
//}