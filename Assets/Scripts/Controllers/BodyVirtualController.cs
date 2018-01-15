using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyVirtualController : MonoBehaviour , IVirtualController {
	private IHealthController healthController;
	private IInteractionController interactionController;
	private ICameraController cameraController;

	private Rigidbody rigidBody;
	private float bodyMass = 54.0f;

	private float cameraSpeed = 3.0f;

	private float movementSpeed = 8.0f;
	private float turnSpeed = 0.1f;
	private float jumpSpeed = 10.0f;

	void Start () {
		healthController = new CombatHealthController ();
		interactionController = new NullInteractionController ();
		cameraController = new ThirdPersonCameraController ( cameraSpeed );

		rigidBody = GetComponent < Rigidbody > ();
		if ( rigidBody != null ) {
			rigidBody.mass = bodyMass;
			rigidBody.freezeRotation = true;
			rigidBody.isKinematic = false;
		}
	}

	void Update () {

	}

	void LateUpdate () {
		
	}

	public virtual void ViewStickInput ( Vector3 input ) {
		if (rigidBody == null) {
			return;
		}

		cameraController.updateCameraPositioning ( 
			input,
			rigidBody.position
		);
	}

	public virtual void MovementStickInput ( Vector3 input ) {
		Move ( input );
	}

	public virtual void RunButton ( Vector3 input ) {
		Move ( input );
	}

	public virtual void Slide ( Vector3 input ) {
		Move ( input );
	}

	public virtual void JumpButton () {
		if ( rigidBody == null ) {
			return;
		}
			
		if ( !onGround () ) {
			return;
		}

		float currentVelocityX = rigidBody.velocity [ 0 ];
		float currentVelocityZ = rigidBody.velocity [ 2 ];

		Vector3 jumpVelocity = new Vector3 (
			currentVelocityX,
			jumpSpeed,
			currentVelocityZ
		);

		rigidBody.velocity = jumpVelocity;
	}

	public virtual void InteractionButton () {

	}

	public virtual void MagicButton () {

	}

	public virtual void TimeSlowButton () {

	}

	public virtual void TimePauseButton () {

	}

	public virtual void TimeStopButton () {

	}

	private void Move ( Vector3 input ) {
		if (rigidBody == null) {
			return;
		}
			
		rotateBodyToCameraView ();

		Vector3 newVelocity = input * movementSpeed;
		if ( !onGround () ) {
			newVelocity *= 0.5f;
		} 
			
		float currentVelocityY = rigidBody.velocity [ 1 ];
		newVelocity [ 1 ] = currentVelocityY;
		rigidBody.velocity = newVelocity;
	}

	private void rotateBodyToCameraView () {
		if (rigidBody == null) {
			return;
		}

		Vector3 cameraRotation = cameraController.getCameraRotation ().eulerAngles;
		cameraRotation [ 0 ] = 0.0f;
		cameraRotation [ 2 ] = 0.0f;

		rigidBody.rotation = Quaternion.Slerp ( 
			rigidBody.rotation,
			Quaternion.Euler ( cameraRotation ),
			Time.time * turnSpeed
		);
	}

	private bool onGround () {
		return rigidBody.velocity [ 1 ] == 0.0f;
	}
}
