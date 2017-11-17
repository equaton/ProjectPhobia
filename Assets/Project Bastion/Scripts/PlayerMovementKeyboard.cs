﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementKeyboard : MonoBehaviour
	{
		public float m_Speed = 12f;                 // How fast the player moves forward and back.
		public float m_TurnSpeed = 180f;            // How fast the player turns in degrees per second.
		public AudioSource m_MovementAudio;         // Reference to the audio source used to play steps sounds. NB: different to the shooting audio source.
		public AudioClip m_PlayerWalking;           // Audio to play when the player is moving.
		public AudioClip m_PlayerIdle;              // Audio to play when the player is idle.
		public float m_PitchRange = 0.2f;           // The amount by which the pitch of the steps noises can vary.

		private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
		private string m_TurnAxisName;              // The name of the input axis for turning.
		private Rigidbody m_Rigidbody;              // Reference used to move the player.
		private float m_MovementInputValue;         // The current value of the movement input.
		private float m_TurnInputValue;             // The current value of the turn input.
		private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
		private ParticleSystem[] m_particleSystems; // References to all the particles systems used by the Player

		private void Awake ()
		{
			m_Rigidbody = GetComponent<Rigidbody> ();
		}


		private void OnEnable ()
		{
			// When the tank is turned on, make sure it's not kinematic.
			m_Rigidbody.isKinematic = false;

			// Also reset the input values.
			m_MovementInputValue = 0f;
			m_TurnInputValue = 0f;

			// We grab all the Particle systems child of that Player to be able to Stop/Play them on Deactivate/Activate
			// It is needed because we move the Player when spawning it, and if the Particle System is playing while we do that
			// it "think" it move from (0,0,0) to the spawn point, creating a huge trail of smoke
			m_particleSystems = GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < m_particleSystems.Length; ++i)
			{
				m_particleSystems[i].Play();
			}
		}


		private void OnDisable ()
		{
			// When the player is turned off, set it to kinematic so it stops moving.
			m_Rigidbody.isKinematic = true;

			// Stop all particle system so it "reset" it's position to the actual one instead of thinking we moved when spawning
			for(int i = 0; i < m_particleSystems.Length; ++i)
			{
				m_particleSystems[i].Stop();
			}
		}


		private void Start ()
		{
			// Initializing axes
			m_MovementAxisName = "Vertical";
			m_TurnAxisName = "Horizontal";

			// Store the original pitch of the audio source.
			m_OriginalPitch = m_MovementAudio.pitch;
		}


		private void Update ()
		{
			// Store the value of both input axes.
			m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
			m_TurnInputValue = Input.GetAxis (m_TurnAxisName);

			StepsAudio ();
		}


		private void StepsAudio ()
		{
			// If there is no input (the tank is stationary)...
			if (Mathf.Abs (m_MovementInputValue) < 0.1f && Mathf.Abs (m_TurnInputValue) < 0.1f)
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
			// Adjust the rigidbodies position and orientation in FixedUpdate.
			Move ();
			Turn ();
		}


		private void Move ()
		{
			// Create a vector in the direction the player is facing with a magnitude based on the input, speed and the time between frames.
			Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

			// Apply this movement to the rigidbody's position.
			m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
		}


		private void Turn ()
		{
			// Determine the number of degrees to be turned based on the input, speed and time between frames.
			float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

			// Make this into a rotation in the y axis.
			Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

			// Apply this rotation to the rigidbody's rotation.
			m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
		}
}
