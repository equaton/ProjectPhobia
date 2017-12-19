using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	private Animator animator;
	private Rigidbody enemyRigidBody;

	// Use this for initialization
	void Start () {

		// Get references from enemy.
		animator = GetComponent<Animator>();
		enemyRigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		// Update the Enemy animator 
		if (enemyRigidBody.velocity.magnitude < 0.1) {
			animator.SetBool ("isRunning", true);

		} else 
		{
			animator.SetBool ("isRunning", false);
		}
	}
}
