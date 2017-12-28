using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoiseMaker : MonoBehaviour {

	public CapsuleCollider soundColliderAI;					// Reference to the Player Collider to simulate noise made. Enemy will collide with the capsule and react.
	public float ColliderRadiusVelocitySmoothTime = 0.3f; 

	private Vector3 playerVelocity;							// Player velocity calculated with the calculate velocity script.
	private float soundColliderRadius;						// How large is the sound collider, calculated depending on the player velocity.
	private float ColliderRadiusVelocity = 0f;


	// Update is called once per frame
	void Update () {

		// Get the player velocity.
		playerVelocity = GetComponent<CalculateVelocity>().currVel;

		// Generate a radius for the noise made depending on the velocity of player.
		soundColliderRadius = Mathf.SmoothDamp(soundColliderRadius,GetComponent<PlayerStatistics>().playerStats.walikngNoiseRadius * playerVelocity.magnitude, ref ColliderRadiusVelocity , 1f); 

		//"Create a sound" activating and deactivating the sound collider.
		soundColliderAI.radius = soundColliderRadius;
		soundColliderAI.enabled = true;

	}
}
