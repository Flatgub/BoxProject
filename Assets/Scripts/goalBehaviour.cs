using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalBehaviour : MonoBehaviour {

	public Sprite satisfiedSprite;
	public Sprite unsatisfiedSprite;
	private SpriteRenderer renderComponent;
	private GameObject[] satisfiers;

	private void Awake() {
		renderComponent = gameObject.GetComponent<SpriteRenderer>();
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool checkSatisfaction() {
		bool satisfied = false;

		satisfiers = GameObject.FindGameObjectsWithTag("goalSatisfier");
		foreach (var obj in satisfiers) {
			if (obj.transform.position == transform.position) {
				satisfied = true;
				break;
			}
		}

		if (satisfied) {
			becomeSatisfied();
		} else {
			becomeUnsatisfied();
		}

		return false;
	}

	public void becomeSatisfied() {
		renderComponent.sprite = satisfiedSprite;
	}

	public void becomeUnsatisfied() {
		renderComponent.sprite = unsatisfiedSprite;
	}
}
