using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : lerpMovement {

	public bool attemptMoveInDirection(Vector3 direction, float speed) {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1);
		if (hit.collider == null) {
			moveTo(transform.position + direction, speed);
			return true;
		}
		else {
			//Attempt to push the object we collided
			GameObject obstacle = hit.transform.gameObject;
			SolidTile tile = obstacle.GetComponent<SolidTile>();
			if (tile != null) {
				if (tile.pushInDirection(direction)) {
					moveTo(transform.position + direction, speed);
					return true;
				}
			}
			return false;
		}

	}

}
