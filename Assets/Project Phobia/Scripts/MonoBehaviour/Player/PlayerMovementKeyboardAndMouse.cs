using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementKeyboardAndMouse : MonoBehaviour
{
	public float m_Speed = 12f;                 // How fast the player moves forward and back.
	public AudioClip m_PlayerWalking;           // Audio to play when the player is moving.
	public AudioClip m_PlayerIdle;              // Audio to play when the player is idle.
	public float m_PitchRange = 0.2f;           // The amount by which the pitch of the steps noises can vary.
	public bool onlyYRotation = true;			// Make the rotation of the Player to happen only on the Y Axis, otherwise allow rotation on X axis too.
    public LineRenderer cursorLine; 			// Reference to the LineRendered component in the Player object
	[HideInInspector]
	public Vector3 m_MovementVelocityValue;     // The current value of the movement input as velocity vector
	public Ray ray;								// Ray of the vector of the cursor from the camera.
	public Transform FrontFacingTransform;		// The transform of the front facing object, usually the gun holder object. It will rotate its x axys to follow the mouse.
	public bool isMovementEnabled = true;		// Boolean used to eble and disable player movement and cursor pointer.
	[Range(0.0f, 1.0f)]
	public float viewingMovementMaximalDetriment;// Number from 0 to 1 to reduce the movement of the player when it is looking backwards.
	public AudioSource m_MovementAudio;         // Reference to the audio source used to play steps sounds. NB: different to the shooting audio source.


	private Rigidbody m_Rigidbody;              // Reference used to move the player.
	private string m_VerticalAxisName;          // The current value of the vertical movement input.
	private string m_HorizontalAxisName;        // The current value of the horizontal moviment input.
	private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
	private ParticleSystem[] m_particleSystems; // References to all the particles systems used by the Player.
	private Camera viewCamera;					// Reference to the main camera scene object.
	private float viewCameraYRotation;			// Reference to the rotation of the Y Axis of the Camera.
	private float angleBwtViewAndMovement;		// Angle between the two vector of player movement and view.
	private	Ray cursorLineRay;


	private void Awake ()
	{
		m_Rigidbody = GetComponent<Rigidbody> ();
		viewCamera = Camera.main;
	}


	private void Start ()
	{
		// Initializing axes
		m_VerticalAxisName = "Vertical";
		m_HorizontalAxisName = "Horizontal";


		// Store the original pitch of the audio source.
		m_OriginalPitch = m_MovementAudio.pitch;

		// Store rotation of the view camera on the Y axis
 		viewCameraYRotation = viewCamera.transform.rotation.eulerAngles.y;

	}

	private void Update ()
	{
		// If the movemenet is enabled, perform the movement!	
		if (isMovementEnabled) {
			// Store the value of both input axes and calculate the amount of movement as in velocity.
			m_MovementVelocityValue = new Vector3 (Input.GetAxisRaw (m_HorizontalAxisName), 0, Input.GetAxisRaw (m_VerticalAxisName)).normalized * m_Speed;

			// Rotate the velocity vector as the Y axis of the Camera so that the movement of the Player is still vertical/horizontal on the camera view.
			m_MovementVelocityValue = Quaternion.Euler (0, viewCameraYRotation, 0) * m_MovementVelocityValue;

			// Get mouse position on screen and turn the GameObject to the direction of the pointer.
			Ray ray = viewCamera.ScreenPointToRay (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
			// Declare a raycast hit to store information about what our raycast has hit
			RaycastHit hit;
			Physics.Raycast (ray, out hit);

			//Make the GameObject look at the collision between the cursor and the ground.
			transform.LookAt (hit.point);

			// Calculate the angle between the vector of movement and the vector of view of the player. The movement will be depending on this.
			angleBwtViewAndMovement = Vector3.Angle(m_MovementVelocityValue, transform.forward);

			// the Player will look at the point in all Axis, if the rotation needs to happen only on the Y axis, then set the onlyYRotation to false
			if (onlyYRotation) {
				transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);
			}

			// Rotate the X axis of the Front facing transform to move the gun holder up and down to follow where the cursor is.
			if (FrontFacingTransform) {
				FrontFacingTransform.LookAt (hit.point);

			}

			//Play walking audio
			StepsAudio ();

			// Draw the laserLine
			//Draw a line from the player to the cursor space in the map
			if (cursorLine) {
				DrawCursorLine ();
			}
		} else 
		{
			// Disable the LineRenderer.
			DisableCursorLine();
		}

			
	}
	
	public void DrawCursorLine()
	{

		if (!cursorLine.enabled) {
			cursorLine.enabled = true;
			// Get a ray from the player to the point where the cursor is. Create a line between that and whatever is hit by that ray.
			cursorLine.positionCount = 2;
		}

		cursorLineRay.origin = FrontFacingTransform.position;
		cursorLineRay.direction = FrontFacingTransform.forward;

		RaycastHit cursorLineHit;

		Physics.Raycast (cursorLineRay, out cursorLineHit);

		cursorLine.SetPosition (0, FrontFacingTransform.position);
		cursorLine.SetPosition (1, cursorLineHit.point);

	}

	// If the player is dead, disable the cursorLine and return.
	private void DisableCursorLine()
	{
		if (cursorLine.enabled)
		{
			// Clear current LineRenderer.
			cursorLine.positionCount = 0;

			// Disable the LineRenderer.
			cursorLine.enabled = false;
		}
	}

	private void StepsAudio ()
	{
		// If there is no input (the tank is stationary)...
		if (Mathf.Abs (m_MovementVelocityValue.magnitude) < 0.1f)
		{
			// ... and if the audio source is currently playing the walking clip...
			if (m_MovementAudio.clip == m_PlayerWalking)
			{
				// ... change the clip to idling and play it.
				m_MovementAudio.clip = m_PlayerIdle;
				m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play ();
			}
		}
		else
		{
			// Otherwise if the player is moving and if the idling clip is currently playing...
			if (m_MovementAudio.clip == m_PlayerIdle)
			{
				// ... change the clip to walking and play.
				m_MovementAudio.clip = m_PlayerWalking;
				m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play();
			}
		}
	}


	private void FixedUpdate ()
	{
		// Adjust the rigidbodies position and orientation in FixedUpdate, if the movement is enabled.
		if (isMovementEnabled)
		{
			Move ();
		}
	}


	private void Move ()
	{
		// Calculate impact of viewving angle on movement.
		float movementImpairment = ((180 - angleBwtViewAndMovement) / 180 + viewingMovementMaximalDetriment);

		// If the impairment value is bigger than 1, make it equal to one so that the player does not run more than it is supposed to.
		if (movementImpairment > 1)
		{
			movementImpairment = 1;
		}

		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + m_MovementVelocityValue  * Time.fixedDeltaTime * movementImpairment);

	}
	
}
