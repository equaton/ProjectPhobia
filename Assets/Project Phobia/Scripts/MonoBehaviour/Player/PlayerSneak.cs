using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSneak : MonoBehaviour {

	public bool isSneaking;						// Is the player sneaking?

	private string sneakButton;					// Reference to mouse key to use to toggle the Flashlight.

	// Use this for initialization
	void Start () {

		sneakButton = "Sneak";
	}

	// Update is called once per frame
	void FixedUpdate () {

		// When right button is clicked, the FlashLight is toggled.
		if (Input.GetButtonDown (sneakButton)) 
		{

			// toggle sneaking on and off!
			isSneaking = !isSneaking;

			GameManager.gameManager.playerManager.Sneak (isSneaking);
			GetComponent<PlayerMovementKeyboardAndMouse> ().isSneaking = isSneaking;
		}	
}
}
