﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// This class is to control what the player can do at any moment, like disable/enable movement and shooting.
[Serializable]
public class PlayerManager 
{

	public Transform spawnPoint;								// Point where to spawn the Player in.
	public RawImage sneakImage;									// Reference to the sneak icon in the UI.
	[HideInInspector] public GameObject playerInstance;			// Referenece to the player instance.
	public static GameManager gameManager;
	[HideInInspector] public PlayerHealth playerhealth;			// Reference to the Player's health script.
	public PlayerStats playerStats; 							// The reference to the Player Stats Scriptable Object.

	private PlayerMovementKeyboardAndMouse playerMovement; 		// Reference to the Player's moving script.
	private PlayerShooting playerShooting;						// Reference to the Plater's Shooting script.

	// Use this for initialization
	public void SetupPlayer ()
	{
		// Get the reference to the Player components.
		playerMovement = playerInstance.GetComponent<PlayerMovementKeyboardAndMouse>();
		playerhealth = playerInstance.GetComponent <PlayerHealth> ();
		playerShooting = playerInstance.GetComponentInChildren<PlayerShooting>();

	}
	
	public void DisableControls()
	{
		//Block the movement controls.
		if (playerMovement != null) 
		{
			playerMovement.isMovementEnabled = false;
			playerMovement.m_MovementAudio.Stop ();
		}

		// Block the shooting.
		if (playerShooting != null) 
		{
			playerShooting.shootingEnabled = false;
		}

	}

	public void EnableControls()
	{
		//Enable the movement controls.
		if (playerMovement != null) 
		{
			playerMovement.isMovementEnabled = true;
			Debug.Log (playerMovement);
		}

		// Enable the shooting.
		if (playerShooting != null) 
		{
			playerShooting.shootingEnabled = true;
		}
	}

	public void Sneak(bool sneakStance)
	{
		sneakImage.enabled = sneakStance;

	}

}
	

