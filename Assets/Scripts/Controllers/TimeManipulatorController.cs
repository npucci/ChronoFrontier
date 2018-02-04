using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulatorController : MonoBehaviour , ITimeManipulatorController {
	private float timeSlowFactor = 0.15f;
	private float timePauseFactor = 0f;
	private float timeRestoreFactor = 1.0f;

	private float timeFieldRadiusMultiplier = 10.0f;
	private SphereCollider timeField;

	void Start () {
		timeField = gameObject.AddComponent < SphereCollider > ();
		timeField.isTrigger = true;
		timeField.radius = timeFieldRadiusMultiplier * timeField.radius;
	}

	public virtual float TimeSlow () {
		return timeSlowFactor;
	}

	public virtual float TimePause () {
		return timePauseFactor;
	}

	public virtual float TimeRestore () {
		return timeRestoreFactor;
	}
}
