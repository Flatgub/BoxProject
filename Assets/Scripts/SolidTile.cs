using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidTile : MonoBehaviour {

	public bool isPushable;
	public bool canMove = false;
	public float movementSpeed;
	private lerpMovement moveComponent;

	// Use this for initialization
	void Awake () {
		moveComponent = GetComponent<lerpMovement>();
		if (moveComponent != null) {
			canMove = true;
		}
	}

	public bool pushInDirection(Vector3 direction) {
		// No point raycasting if we can't move
		if (!canMove || !isPushable) {return false;}
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,1);
		if (hit.collider == null) {
			moveComponent.moveTo(transform.position + direction, movementSpeed);
			return true;
		} else {
			return false;
		}
	}

	// Update is called once per frame
	void Update() {
		if (canMove) {
			moveComponent.processMovement();
		}
	}
}
