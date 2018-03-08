using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[CreateAssetMenu (menuName= "SpawnManager/MultipleEnemySpawner" )]
public class MultipleEnemySpawner : EnemySpawnManager {


	public GameObject SpawnPoint; 			     	// List to the enemy Spawn Position.
	public EnemyManager enemyManager;				// Reference to the Enemy Manager.
	public GameObject enemyPrefab;					// Reference to the Enemy prefab.
	public float timebetweenSpawns = 5000f;	    // Reference to the time between a spawn and another. 
	public float counter =0f; 

	private MonoBehaviour _mb;
	private float timer = 5001f; 

	// Keep Spawning monsters
	public override void StartSpawningEnemies()
	{
		_mb = GameObject.FindObjectOfType<MonoBehaviour>();
		_mb.StartCoroutine (SpawnMonster (enemyPrefab, SpawnPoint.transform));
	}


	// Spawn Enemy with selected prefab and position.
	IEnumerator SpawnMonster(GameObject prefab, Transform position)
	{

		while (true) {

			timer = timer + Time.deltaTime;

			Debug.Log (timer);

			if (timer > timebetweenSpawns) {



				Debug.Log ("Spawn!");

				// Spawn the enemy.
				enemyManager.enemyInstance = GameObject.Instantiate (enemyPrefab, position.position, position.rotation) as GameObject;

				// Set it up.
				enemyManager.SetupEnemy ();


				timer = 0f;
				Debug.Log ("Spawn! " + counter);
				counter = counter + 1;


			}


		}

	}
}
