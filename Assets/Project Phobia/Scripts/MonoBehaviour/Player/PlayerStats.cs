using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "Player/PlayerStats")]
public class PlayerStats : ScriptableObject {
	public float lookRange = 40f;
	public float shootingNoiseRadius = 40f;
	public float walikngNoiseRadius = 10f;
}
