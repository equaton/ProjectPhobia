using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour 
{

	public Text ui_messageText;									// Reference to the text on the canvas to communicate to the user. 
	public float startWaitTime = 4f; 							// Amount of time the game waits before starting.
	public float gameOverTime = 5f;								// Amount of time the game waits before loading after Game Over;
	public PlayerManager playerManager;							// Reference to the playerManager.
	public GameObject playerPrefab;								// The player prefab to instanciate.
	public CameraControl cameraControl;							// Reference to the Camera controller.
	[HideInInspector] public static GameManager gameManager;	// Reference to this GameManager. Used to access from other scripts.
	public GameObject healthBar;								// Reference to the Healtbar GameObject.
	[HideInInspector] public Slider playerHearthSlider;			// Reference to the player health slider.
	public Image playerHitFlashImage;							// Reference to the image that will appear when the player is hit.
	public EnemySpawnManager spawnManager;						// Reference to the Enemy Spawn Manager.


	private WaitForSeconds startWait;							// Used to delay the game start.


	// Use this for initialization
	void Start () 
	{
		// If the instance of gameManager is null, instanciate to itself.
		if (gameManager == null) 
		{
			gameManager = this;
		}

		// Create references
		startWait = new WaitForSeconds (startWaitTime);
		playerHearthSlider = healthBar.GetComponentInChildren<Slider>();

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

		// Setup the Player so that it's getting all the required references. (PlayerManager is NOT a Monobehaviour!)
		playerManager.SetupPlayer ();
	}


	// Main GameLoop for the Game
	private IEnumerator GameLoop()
	{
		yield return StartCoroutine (RoundStarting ());
		yield return StartCoroutine (RoundPlaying ());
		yield return StartCoroutine (GameOver ());

	}
		
	private IEnumerator RoundStarting()
	{

		// Disable Player Controls
		playerManager.DisableControls();

		//Make a welcome Text appear.
		ui_messageText.text = "Project Phobia";

		//Disable the Slider
		healthBar.SetActive( false);

		yield return startWait;

		ui_messageText.text = "";
		playerManager.EnableControls ();

		//Enable the Slider
		healthBar.SetActive (true);

		// Start to spawn enemies.
		spawnManager.StartSpawningEnemies();


	}


	private IEnumerator RoundPlaying()
	{
		// Continue to play until the player is still alive
		while (playerManager.playerhealth.isPlayerAlive == true) 
		{
			yield return null;
		}
	}


	private IEnumerator GameOver()
	{
		// Set the message to GameOver.
		ui_messageText.text = "You Died!";

		// Disable Player controls.
		playerManager.DisableControls();

		// Stop game time at death.
		Time.timeScale = 0;

		// Wait for some seconds before reeboting the scene.
		float pauseTime = Time.realtimeSinceStartup + gameOverTime;
		Debug.Log ("Before wait.");
		while (Time.realtimeSinceStartup < pauseTime)
		{
			yield return null;
		}

		// Start time after the end pause.
		Time.timeScale = 1;

		// Reload the current scene.
		EditorSceneManager.LoadScene ( EditorSceneManager.GetActiveScene().name);
	}
}
