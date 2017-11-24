using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



	public class GameManager : MonoBehaviour 
	{

		public Text ui_messageText;									// Reference to the text on the canvas to communicate to the user. 
		public float startWaitTime = 4f; 							// Amount of time the game waits before starting.
		public float gameOverTime = 5f;								// Amount of time the game waits before loading after Game Over;
		public PlayerManager playerManager;							// Reference to the playerManager.
		public GameObject playerPrefab;								// The player prefab to instanciate.
		public CameraControl cameraControl;							// Reference to the Camera controller.
		[HideInInspector] public static GameManager gameManager;	// Reference to this GameManager. Used to access from other scripts.
		public Slider playerHearthSlider;							// Reference to the player health slider.
		public Image playerHitFlashImage;							// Reference to the image that will appear when the player is hit.


		private WaitForSeconds startWait;							// Used to delay the game start.
		private WaitForSeconds endWait;								// Used to delay the game end.


		// Use this for initialization
		void Start () 
		{
			// If the instance of gameManager is null, instanciate to itself.
			if (gameManager == null) 
			{
				gameManager = this;
			}

			

			// Create references to the times.
			startWait = new WaitForSeconds (startWaitTime);
			endWait = new WaitForSeconds (gameOverTime);

			// Instanciate the Player
			SpawnPlayer();

			// Set the camera target to the Player.
			cameraControl.m_Targets[0] = playerManager.playerInstance.transform;

			// Start GameLoop Coruotine
			StartCoroutine (GameLoop());

		}
		
		// Update is called once per frame
		void SpawnPlayer () 
		{
			// Instanciate the Player.
			playerManager.playerInstance = Instantiate(playerPrefab,playerManager.spawnPoint.position,playerManager.spawnPoint.rotation) as GameObject;
			Debug.Log (playerManager.playerInstance);

			// Setup the Player so that it's getting all the required references. (PlayerManager is NOT a Monobehaviour!)
			playerManager.SetupPlayer ();
		}


		// Main GameLoop for the Game
		private IEnumerator GameLoop()
		{
			yield return StartCoroutine (RoundStarting ());
			//yield return StartCoroutine (RoundPlaying ());
			//yield return StartCoroutine (GameOver ());

		}
			
		private IEnumerator RoundStarting()
		{

			// Disable Player Controls
			playerManager.DisableControls();
			Debug.Log("Player controls Disabled");
			

			//Make a welcome Text appear.
			ui_messageText.text = "Project Phobia";
			Debug.Log ("text");

			yield return startWait;

			ui_messageText.text = "";
			playerManager.EnableControls ();
		}

		/*
		private IEnumerator RoundPlaying()
		{
			yield return;
		}

		private IEnumerator GameOver()
		{
			yield return;
		}*/
	}
