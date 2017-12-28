using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

	public EnemyState currentState;
	public EnemyState remainState;
	public EnemyStats enemyStats; 

	[HideInInspector] public NavMeshAgent navMeshAgent;			// Reference to the enemy navMeshAgent;
	 public Transform enemyTarget;				// Reference to the enemy target;
	[HideInInspector] public float stateTimeElapsed;			// Timer;

	private bool isAiActive;									// Is AI active?


	// Use this for initialization
	void Awake () {

		// Get the references.
		navMeshAgent = GetComponent<NavMeshAgent> ();
		enemyTarget = GameObject.FindGameObjectWithTag ("Player").transform;

	}
	
	// Update is called once per frame
	public void SetupAI (bool aiActivationFromEnemyManager) {
		
		isAiActive = aiActivationFromEnemyManager;

		Debug.Log (isAiActive);

		if (isAiActive) {
			navMeshAgent.enabled = true;
		} 
		else 
		{
			navMeshAgent.enabled = false;
		}
	}

	void Update()
	{
		if (!isAiActive)
			return;
		currentState.UpdateState (this);
	}


	public void TransitionToState(EnemyState nextState)
	{
		if (nextState != remainState) 
		{
			currentState = nextState;
			OnExitState ();
		}
	}

	public bool CheckIfCountdownElapsed (float duration)
	{
		stateTimeElapsed += Time.deltaTime;

		return (stateTimeElapsed >= duration);
	}


	public void OnExitState()
	{
		stateTimeElapsed = 0;
	}

}

