using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GameManager : MonoBehaviour 
{

	public Text ui_messageText;									// Reference to the text on the canvas to communicate to the user. 
	public Text ui_score;										// Reference to the text on the canvas to communicate to the score. 
	public float startWaitTime = 4f; 							// Amount of time the game waits before starting.
	public float gameOverTime = 5f;								// Amount of time the game waits before loading after Game Over;
	public PlayerManager playerManager;							// Reference to the playerManager.
	public GameObject playerPrefab;								// The player prefab to instanciate.
	public GameObject pickupitem;								// The pickup item prefab
	public CameraControl cameraControl;							// Reference to the Camera controller.
	[HideInInspector] public static GameManager gameManager;	// Reference to this GameManager. Used to access from other scripts.
	public GameObject healthBar;								// Reference to the Healtbar GameObject.
	[HideInInspector] public Slider playerHearthSlider;			// Reference to the player health slider.
	public Image playerHitFlashImage;							// Reference to the image that will appear when the player is hit.
	public EnemySpawnManager spawnManager;						// Reference to the Enemy Spawn Manager.
	public bool collectedVictoryIteam = false;					// Did the player collected the victory item?
	public float playerpoints = 0;								// Player points


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

		if (collectedVictoryIteam) {
			yield return StartCoroutine (GameWon ());
		} else {
			yield return StartCoroutine (GameOver ());
		}

	}
		
	private IEnumerator RoundStarting()
	{

		// Disable Player Controls
		playerManager.DisableControls();

		//Make a welcome Text appear.
		ui_messageText.text = "Project Phobia";
		ui_score.text = playerpoints.ToString();

		//Disable the Slider
		healthBar.SetActive( false);

		yield return startWait;

		ui_messageText.text = "";
		playerManager.EnableControls ();

		//Enable the Slider
		healthBar.SetActive (true);

		spawnManager.StartSpawningEnemies();

	}


	private IEnumerator RoundPlaying()
	{

		// Continue to play until the player is still alive
		while (playerManager.playerhealth.isPlayerAlive == true ) 
		{
			 

			//If the pickup item is collected, add a point and generate a new one
			if (collectedVictoryIteam == true) 
			{


				Instantiate(pickupitem,GetRandomLocation(),playerManager.spawnPoint.rotation);
				spawnManager.StartSpawningEnemies();

				playerpoints = playerpoints + 1;
				ui_score.text = playerpoints.ToString();
				collectedVictoryIteam = false;
			}


			yield return null;
		}
	}


	private IEnumerator GameOver()
	{
		// Set the message to GameOver.
		ui_messageText.text = "You Died!";

		// Disable Player controls.
		playerManager.DisableControls();

		// Play GameOver sounds.
		playerManager.playerhealth.playerAudio.clip = playerManager.playerhealth.playerDeathAudio;
		playerManager.playerhealth.playerAudio.Play ();

		// Stop game time at death.
		Time.timeScale = 0;

		// Wait for some seconds before reeboting the scene.
		float pauseTime = Time.realtimeSinceStartup + gameOverTime;
		while (Time.realtimeSinceStartup < pauseTime)
		{
			yield return null;
		}

		// Start time after the end pause.
		Time.timeScale = 1;

		// Reload the current scene.
		SceneManager.LoadScene ( SceneManager.GetActiveScene().name);
	}

	private IEnumerator GameWon()
	{
		// Set the message to GameOver.
		ui_messageText.text = "You Survived!";

		// Disable Player controls.
		playerManager.DisableControls();

		// Stop game time at death.
		Time.timeScale = 0;

		// Wait for some seconds before reeboting the scene.
		float pauseTime = Time.realtimeSinceStartup + gameOverTime;
		while (Time.realtimeSinceStartup < pauseTime)
		{
			yield return null;
		}

		// Start time after the end pause.
		Time.timeScale = 1;

		// Reload the current scene.
		SceneManager.LoadScene ( SceneManager.GetActiveScene().name);

	}

	// Generate random location for pickup item
	private Vector3 GetRandomLocation ()
	{

		NavMeshTriangulation navMeshData =	NavMesh.CalculateTriangulation ();

		// Pick the first indice of a random triangle in the nav mesh
		int t = Random.Range (0, navMeshData.indices.Length - 3);

		// Select a random point on it
		Vector3 point = Vector3.Lerp (navMeshData.vertices [navMeshData.indices [t]], navMeshData.vertices [navMeshData.indices [t + 1]], Random.value);
		Vector3.Lerp (point, navMeshData.vertices [navMeshData.indices [t + 2]], Random.value);

		return point;
	}

}
