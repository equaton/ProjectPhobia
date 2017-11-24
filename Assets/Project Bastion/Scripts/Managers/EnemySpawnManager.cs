using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EnemySpawnManager: ScriptableObject  
{

	// Spawn Enemy with selected prefab and position.
	public abstract void SpawnMonster(GameObject prefab, Transform position);
	public abstract void StartSpawningEnemies ();
}
