using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This Script will calculate the velocity of the Kinetic Object.

public class CalculateVelocity : MonoBehaviour {

	[HideInInspector]
	public Vector3 currVel;					// Current velocity of the Object.

	// Use this for initialization
	void Awake () {
		StartCoroutine (CalcVelocity ());
	}
	
	IEnumerator CalcVelocity()
	{
		while (Application.isPlaying) {
			// Position at the frame start.
			Vector3 prevPos = (transform.position) ;

			// Wait till it the end of the frame.
			yield return new WaitForEndOfFrame ();

			// Calculate velocity

			currVel = ((prevPos - transform.position) / Time.deltaTime);
	}
}
}
