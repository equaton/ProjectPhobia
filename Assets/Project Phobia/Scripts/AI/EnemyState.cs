using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

[CreateAssetMenu (menuName = "EnemyAI/State")]
public class EnemyState : ScriptableObject {

	public EnemyAction[] actions;
	public EnemyTransition[] transition;
	public Color sceneGizmoColor = Color.gray;

	public void UpdateState(StateController controller)
	{
		DoActions (controller);
		CheckTransitions (controller);
	}

	private void DoActions(StateController controller)
	{
		for (int i = 0; i < actions.Length; i++) {
			actions[i].Act (controller);
		}
	}

	//check all decisions in the State
	private void CheckTransitions(StateController controller)
	{
		for (int i = 0; i < transition.Length; i++) {
			bool decisionSucceded = transition [i].decision.Decide (controller);

			if (decisionSucceded) 
			{
				controller.TransitionToState (transition [i].trueState);
			} else
			{
				controller.TransitionToState (transition[i].falseState);
			}
		}
	}
}