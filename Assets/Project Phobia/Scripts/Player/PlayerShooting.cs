using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShooting : MonoBehaviour
{
	public AudioSource m_ShootingAudio;			// Reference to the audio source used to play the shooting audio. 
	public AudioClip m_FireClip;				// Audio that plays when each shot is fired.
	public Light gunLight;   					// Reference to the gun light. 
	public float reloadingTime = 0.15f;			// How much time needed to fire another time.
	public float bulletDamage = 20;				// How much damage the bullet will do to the enemy.
	public float gunRange = 100f;				// How far away the gun is shooting.
	public LayerMask shootableLayer;			// What is the layer the player can shoot to?
	public LineRenderer gunLine;				// The Line renderer that will create the shooting line.
	public bool	shootingEnabled = true;				// Can the Player shoot?

	private string m_FireButton;				// The input axis that is used for launching projectiles.
	private float timer;						// A timer to decide when to fire.
	private float effectsDisplayTime = 0.15f;   // The proportion of the timeBetweenBullets that the effects will display for.
	private RaycastHit gunHit;					// Reference to the Raycast used to calculate the ray of the shooting.	
	private Ray shootRay;						// Reference for the vector used to do the ray for the shooting.
	private PlayerHealth playerHealth;

	private void Awake ()
	{
		// Define the fire axis .
		m_FireButton = "Fire1";

		// Get a reference to the player health.
		playerHealth = GetComponentInParent<PlayerHealth>();

	}


	private void Update ()
	{
		if (shootingEnabled)
		{

			// Add the time since Update was last called to the timer.
			timer += Time.deltaTime;

			// if the fire button is pressed and the player is still alive
			if (Input.GetButtonDown (m_FireButton) && timer >= reloadingTime && playerHealth.isPlayerAlive) {

				// Fire the gun
				Fire ();
			}

			// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
			if (timer >= reloadingTime * effectsDisplayTime) {
				// ... disable the effects.
				DisableEffects ();
			}
		}

	}

	public void DisableEffects ()
	{
		// Disable the line renderer and the light.
		gunLine.enabled = false;
		gunLight.enabled = false;
	}


	public void Fire ()
	{

		// Reset the timer.
		timer = 0f;

		// Change the clip to the firing clip and play it.
		m_ShootingAudio.clip = m_FireClip;
		m_ShootingAudio.Play ();


		//Set the starting point of the shooting line on the forward transform position
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);

		// Activate the light of the gun
		gunLight.enabled = true;

		//Create a Raycast from the FireTransform
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;


		if (Physics.Raycast (shootRay, out gunHit, gunRange, shootableLayer)) {
			// Try and find an EnemyHealth script on the gameobject hit.
			EnemyHealth enemyHealth = gunHit.collider.GetComponent <EnemyHealth> ();

				// If the EnemyHealth component exist...
				if(enemyHealth != null)
				{
					// ... the enemy should take damage.
				enemyHealth.TakeDamage (bulletDamage, gunHit.point);
				} 

			// Set the second position of the line renderer to the point the raycast hit.
			gunLine.SetPosition (1, gunHit.point);
		}

			// If the raycast didn't hit anything on the shootable layer...
			else {
			// ... set the second position of the line renderer to the fullest extent of the gun's range.
			gunLine.SetPosition (1, transform.position + transform.forward * gunRange);
		}


}
}