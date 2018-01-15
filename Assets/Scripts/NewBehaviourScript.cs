using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	private playerMovement moveComponent;
	private Tween scaleAnimation;
	private SpriteRenderer renderComponent;
	public float movementSpeed;

	// Use this for initialization
	void Awake() {
		moveComponent = GetComponent<playerMovement>();
		renderComponent = GetComponent<SpriteRenderer>();
		scaleAnimation = new Tween(1, 1, 1f);
	}

	// Update is called once per frame
	void Update() {
		handleUserInputs();
		moveComponent.processMovement();

		//Handle animation tween
		scaleAnimation.Advance(Time.deltaTime);
		transform.localScale = new Vector2(scaleAnimation.Evaluate(), 1);
	}

	void handleUserInputs() {
		if (!moveComponent.isMoving) {
			if (Input.GetKey("up")) { moveComponent.attemptMoveInDirection(new Vector3(0, 1), movementSpeed); };
			if (Input.GetKey("down")) { moveComponent.attemptMoveInDirection(new Vector3(0, -1), movementSpeed); };
			if (Input.GetKey("right")) {
				moveComponent.attemptMoveInDirection(new Vector3(1, 0), movementSpeed);
				scaleAnimation.Redefine(transform.localScale.x, 1, movementSpeed*0.75f);
			};
			if (Input.GetKey("left")) {
				moveComponent.attemptMoveInDirection(new Vector3(-1, 0), movementSpeed);
				scaleAnimation.Redefine(transform.localScale.x, -1, movementSpeed*0.75f);
			};
		}
	}	
	
}
