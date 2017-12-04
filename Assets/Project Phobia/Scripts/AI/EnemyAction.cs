using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
// Abstract class to define what the Enemy will do.
public abstract class EnemyAction : ScriptableObject {

	// Act class
	public abstract void Act(StateController controller);

}
