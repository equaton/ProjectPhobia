using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "EnemyAI/EnemyStats")]
public class EnemyStats : ScriptableObject 
{
	public float lookRange = 40f;
	public float lookSpereCastRadius = 1f;
	public float hearingSphereRange = 40f;
}