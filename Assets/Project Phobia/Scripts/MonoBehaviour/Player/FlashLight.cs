using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour {


	public List<Light> lights;					// Reference to the lights of the player Flashlight
	public SoundCollection toggleSounds;		// List of sounds to play when flashlight is toggled.
	public Collider AILight;					// Reference to the player's lightAI component.
	public Collider AILightAura;				// Reference to the player's lightAIAura component.

	private string flashButton;					// Reference to mouse key to use to toggle the Flashlight.
	private AudioSource playerAudio;			// Refrence to the player's AudioSource.



	// Use this for initialization
	void Start () {

		flashButton = "Fire2";
		playerAudio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		// When right button is clicked, the FlashLight is toggled.
		if (Input.GetButtonDown (flashButton)) 
		{
			// go through all the lights in the Player Object
			for (int i = 0; i < lights.Count; i++) {

				// if the lights are off - turn them on and vice versa.
				if (lights [i].enabled) 
				{
					lights [i].enabled = false;
					AILight.enabled = false;
					AILightAura.enabled = false;

				} else 
				{
					lights [i].enabled = true;
					AILight.enabled = true;
					AILightAura.enabled = true;
				}
					

			}

			// Execute a sound when the FlashLight is toggled.
			playerAudio.clip = toggleSounds.soundCollection[Random.Range( 0, toggleSounds.soundCollection.Count)];
			playerAudio.Play ();


		}	

	}
}
