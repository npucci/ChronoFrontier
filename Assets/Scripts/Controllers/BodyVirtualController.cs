using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Niccolo Pucci
 * Purpose:
 * Generic Finite-State Machine for game characters, 
 * that can be used for the player and NPC/AI
 * Use: Attach to desired mesh (with rigidbody)
*/

public class BodyVirtualController : MonoBehaviour , IVirtualController {
	private IHealthController healthController;
	private IInteractionController interactionController;
	private ICameraController cameraController;

	private Rigidbody rigidBody;
	private float bodyMass = 54.0f;

	private float cameraSpeed = 3.0f;

	private float movementSpeed = 12.0f;
	private float turnSpeed = 0.2f;
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

	public virtual void ViewStickInput (
		float xViewStickInput,
		float yViewStickInput
	) {
		if (rigidBody == null) {
			return;
		}

		cameraController.updateCameraPositioning ( 
			xViewStickInput,
			yViewStickInput, 
			rigidBody.position
		);
	}

	public virtual void MovementStickInput (
		float horizontalMovementStickInput,
		float verticalMovementStickInput
	) {
		Move ( 
			horizontalMovementStickInput,
			verticalMovementStickInput
		);
	}

	public virtual void RunButton (
		float horizontalMovementStickInput,
		float verticalMovementStickInput
	) {
		Move ( 
			horizontalMovementStickInput,
			verticalMovementStickInput
		);
	}

	public virtual void Slide (
		float horizontalMovementStickInput,
		float verticalMovementStickInput
	) {
		Move ( 
			horizontalMovementStickInput,
			verticalMovementStickInput
		);
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

	private void Move (
		float horizontalMovementStickInput,
		float verticalMovementStickInput
	) {
		bool noMovementInput = horizontalMovementStickInput == 0.0f && verticalMovementStickInput == 0.0f;
		if ( rigidBody == null || noMovementInput ) {
			return;
		}

		// 1. get forward direction of camera
		Vector3 cameraForwardDirection = cameraController.getCameraForwardDirection ();

		// 2. get side direction of camera
		Vector3 cameraSideDirection = cameraController.getCameraSideDirection ();

		// 3. calculate new forward velocity
		Vector3 newForwardVelocity = cameraForwardDirection * verticalMovementStickInput * movementSpeed;

		// 4. calculate new side velocity
		Vector3 newSideVelocity = cameraSideDirection * horizontalMovementStickInput * movementSpeed;

		// 5. calculate new velocity
		Vector3 newVelocity = newForwardVelocity + newSideVelocity;

		// 6. maintain original y-axis velocity, i.e. gravity or jumping 
		newVelocity.y = rigidBody.velocity.y;

		// 7. calculate Slerp for new rotation, between the original velocity and the new velocity
		Quaternion newRotation = Quaternion.Slerp ( 
			rigidBody.rotation,
			Quaternion.LookRotation ( 
				newVelocity, 
				Vector3.up 
			),
			turnSpeed 
		);

		// 8. apply new rotation to rigidBody 
		rigidBody.rotation = newRotation;

		// 9. apply new velocity to rigidBody
		rigidBody.velocity = newVelocity;
	}

	private void rotateBodyWithCameraView () {
		if (rigidBody == null) {
			return;
		}
			
		Vector3 cameraRotation = Vector3.zero;// cameraController.getCameraRotation ().eulerAngles;
		Vector3 newRotation = new Vector3 (
			rigidBody.rotation.x, 
			cameraRotation.y, 
			rigidBody.rotation.z
		);

		rigidBody.rotation = Quaternion.Slerp ( 
			rigidBody.rotation,
			Quaternion.Euler ( newRotation ),
			turnSpeed * Time.deltaTime
		);

	}

	private bool onGround () {
		return rigidBody.velocity [ 1 ] == 0.0f;
	}
}
