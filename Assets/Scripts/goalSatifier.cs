using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalSatifier : MonoBehaviour {

	private lerpMovement moveComponent;
	private Transform currentGoal;
	private GameObject goalManager;
	private goalManager manager;

	public delegate void satisfierUpdate();
	public static event satisfierUpdate onAllGoalsSatisfied;

	void Awake () {
		//Get the goalManager from the levelManager
		var levelManager = GameObject.Find("levelManager");
		var managerComponent = levelManager.GetComponent<levelManager>();
		goalManager = managerComponent.goalManager;
		manager = goalManager.GetComponent<goalManager>();

		//Establish movement
		moveComponent = GetComponent<lerpMovement>();
		if (moveComponent == null) { //The gameObject is set up wrong
			Debug.LogError("goalSatisifer is immobile!");
			enabled = false;
		}
	}

	private void OnEnable() {
		moveComponent.actionsOnArrival += CheckForGoals;
	}

	private void OnDisable() {
		moveComponent.actionsOnArrival -= CheckForGoals;
	}

	void CheckForGoals() {
		if (currentGoal != null) {//if the satisfier moved it cannot possibly be on the goal anymore
			manager.unsatisfiedGoals.Add(currentGoal);
			//Unsatisfy the crate
			currentGoal.SendMessage("becomeUnsatisfied");
			currentGoal = null;
		}

		foreach (var goal in manager.unsatisfiedGoals) {
			if (goal.position == transform.position) {
				currentGoal = goal;
				manager.unsatisfiedGoals.Remove(goal);
				//Satisfy the crate
				goal.SendMessage("becomeSatisfied");
				

				//Check for completion
				if (manager.unsatisfiedGoals.Count == 0) {
					if (onAllGoalsSatisfied != null) {
						onAllGoalsSatisfied();
					}
				}

				break;
			}
		}

		
	}
		
}
