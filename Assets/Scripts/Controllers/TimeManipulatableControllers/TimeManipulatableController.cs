using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulatableController : MonoBehaviour , ITimeManipulatableController {
	private IVirtualController virtualController;
	private float currentTimeFactor = 1f;
	private Rigidbody rigidBody;

	void Start () {
		virtualController = GetComponent < IVirtualController > ();
		if ( virtualController == null ) {
			virtualController = new NullVirtualController ();	
		}

		rigidBody = GetComponent < Rigidbody > ();
	}

	public virtual void UpdateTimeFactor ( float timeFactor ) {
		currentTimeFactor = timeFactor;

		if ( rigidBody == null ) {
			return;
		}

		if ( timeFactor == 0f ) {
			rigidBody.isKinematic = true;
		} 

		else {
			virtualController.SetRigidbodyProperties ();
		}

	}

	public virtual float TimeFactor () {
		return currentTimeFactor;
	}
}
