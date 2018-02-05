using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInputController : MonoBehaviour , IInputController {
	private Vector3 travelOriginPoint;
	private float travelDistanceMax;

	private float xMovementStickInput;
	private float yMovementStickInput;

	private ICameraController cameraController;
	private IVirtualController virtualController;

	void Start () {
		travelOriginPoint = transform.position;
		travelDistanceMax = 20.0f;

		xMovementStickInput = 1.0f;
		yMovementStickInput = 0.0f;

		virtualController = GetComponent < IVirtualController > ();
		if ( virtualController == null ) {
			virtualController = new NullVirtualController ();
		}

		cameraController = new NullCameraController ();
	}

	// rigidbody and physics calculations 
	void FixedUpdate () {
		float distFromOriginX = Mathf.Abs ( travelOriginPoint.x - transform.position.x );
		//float distFromOriginY = Mathf.Abs ( travelOriginPoint.y - transform.position.y );
		//float distFromOriginZ = Mathf.Abs ( travelOriginPoint.z - transform.position.z );

		if ( travelDistanceMax <= distFromOriginX ) {
			reverseDirection ();
		} 

		bool newMovementInput = xMovementStickInput != 0.0f || yMovementStickInput != 0.0f;
		if ( newMovementInput ) {
			virtualController.MovementStickInput (
				xMovementStickInput,
				yMovementStickInput,
				cameraController.getCameraForwardDirection (),
				cameraController.getCameraSideDirection ()
			);
		}
	}

	public virtual void SetCameraController ( ICameraController cameraController ) {
		// do nothing
	}

	public virtual Vector3 GetPosition () {
		return virtualController.GetPosition ();
	}

	private void reverseDirection () {
		xMovementStickInput *= -1f;
		yMovementStickInput *= -1f;
	}

	void OnCollisionEnter ( Collision collision ) {
		reverseDirection ();
	}
}