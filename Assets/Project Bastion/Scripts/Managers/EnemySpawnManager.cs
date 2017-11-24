﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawnManager: MonoBehaviour  {


	public Transform enemySpawnPoint; 				// List to the enemy Spawn Position.
	public EnemyManager enemyManager;				// Reference to the Enemy Manager.
	public GameObject enemyPrefab;					// Reference to the Enemy prefab.
	public float timebetweenSpawns = 5f;			// Reference to the time between a spawn and another. 


	// Keep Spawning monsters
	public void StartSpawningEnemies()
	{
		Debug.Log ("Starting coroutine for enemy spawn");
		SpawnMonster (enemyPrefab, enemySpawnPoint);
	}
		
	// Spawn Enemy with selected prefab and position.
	public void SpawnMonster(GameObject prefab, Transform position)
	{
		enemyManager.enemyInstance = GameObject.Instantiate(enemyPrefab, position.position, position.rotation) as GameObject;
	}
		
}