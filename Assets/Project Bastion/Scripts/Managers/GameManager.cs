using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace complete 
{

	public class GameManager : MonoBehaviour 
	{

		public Text ui_messageText;				// Reference to the text on the canvas to communicate to the user. 
		public float startWaitTime = 4f; 		// Amount of time the game waits before starting.
		public float gameOverTime = 5f;			// Amount of time the game waits before loading after Game Over;
		public PlayerManager playerManager;		// Reference to the playerManager.
		public GameObject playerPrefab;			// The player prefab to instanciate.
		public CameraControl cameraControl;		// Reference to the Camera controller.

		private WaitForSeconds startWait;		// Used to delay the game start.
		private WaitForSeconds endWait;			// Used to delay the game end.


		// Use this for initialization
		void Start () 
		{
			// Create references to the times.
			startWait = new WaitForSeconds (startWaitTime);
			endWait = new WaitForSeconds (gameOverTime);

			// Instanciate the Player
			SpawnPlayer();

			// Set the camera target to the Player.
			cameraControl.m_Targets[0] = playerManager.playerInstance.transform;

			// Start GameLoop Coruotine
		//	StartCoroutine (GameLoop());

		}
		
		// Update is called once per frame
		void SpawnPlayer () 
		{
			// Instanciate the Player.
			playerManager.playerInstance = Instantiate(playerPrefab,playerManager.spawnPoint.position,playerManager.spawnPoint.rotation) as GameObject;
		}


	/*	// Main GameLoop for the Game
		private IEnumerator GameLoop()
		{
			yield return StartCoroutine (RoundStarting ());
			yield return StartCoroutine (RoundStarting ());
			yield return StartCoroutine (RoundStarting ());

		}*/
	/*		
		private IEnumerator RoundStarting()
		{
			yield return;
		}

		private IEnumerator RoundPlaying()
		{
			yield return;
		}

		private IEnumerator GameOver()
		{
			yield return;
		}*/
	}
}