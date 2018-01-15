using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween {

	private float startValue;
	private float endValue;
	private float duration;
	private float progress = 0.0f;
	private static AnimationCurve tweenCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

	public Tween(float start, float end, float length) {
		startValue = start;
		endValue = end;
		duration = length;		
	}

	public float Evaluate() {
		return Mathf.Lerp(startValue, endValue, tweenCurve.Evaluate(progress/duration));
	}

	public void Advance(float step) {
		//Prevent overflow of bounds
		progress = Mathf.Min(progress + step, duration);
	}

	public bool IsComplete() {
		if (progress == duration) 
		{
			return true;
		}
		return false;
	}

	public void Redefine(float newStart, float newEnd, float newLength) {
		startValue = newStart;
		endValue = newEnd;
		duration = newLength;
		progress = 0.0f;
	}

}
