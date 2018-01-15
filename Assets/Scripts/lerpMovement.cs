using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpMovement : MonoBehaviour {

	private Vector3 movementOrigin;
	private Vector3 movementTarget;
	private float lerpProgress;
	private float lerpTimeLength;
	public bool isMoving;

	//Used to trigger all functions that need to occur whenever this object finishes moving
	public delegate void arrivalAction();
	public arrivalAction actionsOnArrival;

	[SerializeField]
	AnimationCurve lerpCurve;

	
	void Awake () {
		movementOrigin = transform.position;
		movementTarget = transform.position;
		lerpProgress = 0;
		lerpTimeLength = 0;
		isMoving = false;
	}

	public void moveTo(Vector3 target, float speed) {
		movementOrigin = transform.position;
		movementTarget = target;
		lerpProgress = 0;
		lerpTimeLength = speed;
		
	}

	public void processMovement() {
		if (lerpProgress < lerpTimeLength) {
			//limit lerpProgress within bounds
			lerpProgress += Time.deltaTime;
			if (lerpProgress > lerpTimeLength) { lerpProgress = lerpTimeLength; };

			//lerp between the starting location and the target location
			//transform.position = Vector3.Lerp(movementOrigin, movementTarget, lerpProgress / lerpTimeLength);
			transform.position = Vector3.Lerp(movementOrigin, movementTarget, lerpCurve.Evaluate(lerpProgress / lerpTimeLength));

			

			if (lerpProgress == lerpTimeLength) 
			{
				isMoving = false;

				

				if (actionsOnArrival != null) {
					actionsOnArrival();
					Debug.Log("event fired");

				}
			}
			else 
			{
				isMoving = true;
			}
		}
	}

}
