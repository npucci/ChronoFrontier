using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulatableController : MonoBehaviour , ITimeManipulatableController {
	float currentTimeFactor = 1f;
	Rigidbody rigidBody;

	void Start () {
		rigidBody = GetComponent < Rigidbody > ();
	}

	public virtual void UpdateTimeFactor ( float timeFactor ) {
		currentTimeFactor = timeFactor;

		if ( rigidBody == null ) {
			return;
		}

		if ( timeFactor == 0f ) {
			rigidBody.isKinematic = true;
			rigidBody.useGravity = false;
		} 

		else {
			rigidBody.isKinematic = false;
			rigidBody.useGravity = true;
		}

	}

	public virtual float TimeFactor () {
		return currentTimeFactor;
	}
}
