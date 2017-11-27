using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class to define what the Enemy will do.
public abstract class EnemyAction : ScriptableObject {

	// Act class
	public abstract void Act(StateController controller);

}
