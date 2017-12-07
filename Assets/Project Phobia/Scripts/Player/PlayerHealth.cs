using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public float startingHealth = 100;				// The starting health of the player.
	[HideInInspector]
	public bool isPlayerAlive = true;				// Is the player alive or dead?
	public AudioClip playerHitAudio;				// Clip to play when player is hit.
	public AudioClip playerDeathAudio;				// Clip to play when player is dead.
	public Slider playerHearthSlider;				// Reference to the player health slider.
	public Image playerHitFlashImage;				// Reference to the image that will appear when the player is hit.
	public float HitFlashAlpha = 80f;				// Amount of the alpha component of the playerHitFlashImage.
	public float HitFlashFadeTime = 3f ;			// Amount of time for the playerHitImage to disappear.
	public Color HitFlashColor = new Color (255f,0f,0f);

	private float currentHealth;					// The current health of the player.
	private AudioSource playerAudio;				// Reference to the Audio Source of the Enemy.
	private CameraShake cameraShake;				// Reference to the Camera Shake Script


	// Use this for initialization
	void Awake () {
		
		// Set the current health as the starting health. 
		currentHealth = startingHealth;

		// Get the set of references.
		playerAudio = GetComponentInChildren<AudioSource> ();
		playerHearthSlider = GameManager.gameManager.playerHearthSlider;
		playerHitFlashImage = GameManager.gameManager.playerHitFlashImage;
		cameraShake = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake> ();

		//Setup the Player Health Slider.
		playerHearthSlider.minValue = 0;
		playerHearthSlider.maxValue = startingHealth;
		playerHearthSlider.value = currentHealth;


 	}
	
	// Update 	is called once per frame
	void Update () {

		// Gradually remove the flash image over time
		playerHitFlashImage.color = Color.Lerp (playerHitFlashImage.color , Color.clear, HitFlashFadeTime*Time.deltaTime);

	}


	public void TakeDamage (float damage)
	{
		// Is the player already dead? If yes don't do anything!
		if (!isPlayerAlive) {
			return;
		}


		// Remove health based on damage of enemy;
		currentHealth -= damage; 

		// Update the health slider.
		playerHearthSlider.value = currentHealth;

		// Make the flash image appear when the player is hit.
		playerHitFlashImage.color = HitFlashColor;


		// Make the camera shake!
		cameraShake.ShakeCamera();

		// Check if the Player is dead.
		if (currentHealth < 0 || currentHealth == 0) {
			// Perform a set of action	s that occurr on enemy death
			PlayerDeath ();
		} 

		// otherwise play the hit sound and move on.
		else 
		{
			// Set AudioSource clip to EnemyHit and play
			playerAudio.clip = playerHitAudio;
			playerAudio.Play ();
		}

	}

	void PlayerDeath()
	{
		// Player is dead!
		isPlayerAlive = false;

		// Set AudioSource clip to EnemyDeath and play
		playerAudio.clip = playerDeathAudio;
		playerAudio.Play ();

		// Disable controls on death.

	}
		

}
