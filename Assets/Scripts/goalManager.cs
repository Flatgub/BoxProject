using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goalManager : MonoBehaviour {

	public List<GameObject> allGoals;
	public List<Transform> unsatisfiedGoals;
	
	// Use this for initialization
	public void discoverGoals() {
		
		unsatisfiedGoals = new List<Transform>(GameObject.FindGameObjectsWithTag("goal").Select(g => g.transform));

	}

	private void Update() {
		if (Input.GetKey("r")) {
			levelRestart();
		}
	}

	void OnEnable() {
		goalSatifier.onAllGoalsSatisfied += levelCompleted;
	}

	private void OnDisable() {
		goalSatifier.onAllGoalsSatisfied -= levelCompleted;
	}

	void levelCompleted() {
		Debug.Log("you win!");
		}

	void levelRestart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
