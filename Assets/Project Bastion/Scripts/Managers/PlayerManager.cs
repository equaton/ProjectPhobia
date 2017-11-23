using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace complete {
	// This class is to control what the player can do at any moment, like disable/enable movement and shooting.
	[Serializable]
	public class PlayerManager 
	{

		public Transform spawnPoint;								// Point where to spawn the Player in.
		[HideInInspector] public GameObject playerInstance;			// Referenece to the player instance.

		private PlayerMovementKeyboardAndMouse playerMovement; 		// Reference to the Player's moving script.
		private PlayerHealth playerhealth;							// Reference to the Player's health script.

		// Use this for initialization
		void Awake () {
			
			// Get the reference to the Player components.
			playerInstance = GameObject.FindGameObjectWithTag ("Player");
			playerMovement = playerInstance.GetComponent<PlayerMovementKeyboardAndMouse>();
			playerhealth = playerInstance.GetComponent <PlayerHealth> ();
		}
		
		public void DisableControlsOnDeath()
		{
			//Block the movement controls.
			if (playerMovement != null) 
			{
				playerMovement.isMovementEnabled = false;
			}
		}
	}
}