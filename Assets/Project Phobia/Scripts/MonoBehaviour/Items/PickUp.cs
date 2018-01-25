using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to make the pickup item collectabe from the player.

public class PickUp : MonoBehaviour {


	void OnTriggerEnter(Collider other)
	{

		//is the Collider hit the Player's?
		if (other.gameObject.tag == "Player") 
		{
			// send a message to the GameManager about the collection of the item.
			GameManager.gameManager.collectedVictoryIteam = true;

			// destroy the GameObject.
			GameObject.Destroy (this);
		}
	} 
}
