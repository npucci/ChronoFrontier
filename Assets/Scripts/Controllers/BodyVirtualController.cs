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

	private float cameraSpeed = 8.2f;

	private float movementSpeed = 12.0f;
	private float turnSpeed = 15.2f;
	private float jumpSpeed = 10.0f;

	private BodyState bodyState = BodyState.Idle;

	// vertical state: grounded, falling, rising
	// movement state: running, walking, jumping, sliding, none
	// activity state: sleeping, interacting, attacking, moving, idle
	// combat state: sword attacking, magic attacking, time slowing, time pausing, time stopping
	// time state: time slowed, time paused, time stopped

	private enum BodyState {
		Idle,
		Walking,
		Running,
		Moving,
		Jumping,
		Sliding,
		Sleeping,
		SwordAttacking,
		MagicAttacking,
		Interacting,
		TimeSlowing,
		TimePausing,
		TimeStopping
	}

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
		bool noVelocity = rigidBody.velocity.x == 0.0f && 
			rigidBody.velocity.y == 0.0f && 
			rigidBody.velocity.z == 0.0f;
		
		if ( noVelocity ) {
			bodyState = BodyState.Idle;
		} 

		else {
			bodyState = BodyState.Moving;
		}

		Debug.Log ( "bodyState = " + bodyState );
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
		bodyState = BodyState.Running;
	}

	public virtual void Slide (
		float horizontalMovementStickInput,
		float verticalMovementStickInput
	) {
		Move ( 
			horizontalMovementStickInput,
			verticalMovementStickInput
		);
		bodyState = BodyState.Sliding;
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
		bodyState = BodyState.Jumping;
	}

	public virtual void InteractionButton () {
		bodyState = BodyState.Interacting;
	}

	public virtual void MagicButton () {
		bodyState = BodyState.MagicAttacking;
	}

	public virtual void TimeSlowButton () {
		bodyState = BodyState.TimeSlowing;
	}

	public virtual void TimePauseButton () {
		bodyState = BodyState.TimePausing;
	}

	public virtual void TimeStopButton () {
		bodyState = BodyState.TimeStopping;
	}

	private void Move (
		float horizontalMovementStickInput,
		float verticalMovementStickInput
	) {
		
		if ( rigidBody == null ) {
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
			turnSpeed * Time.deltaTime
		);

		// 8. maintain original xz-axis rotation
		newRotation.x = rigidBody.rotation.x;
		newRotation.z = rigidBody.rotation.z;

		// 9. apply new rotation to rigidBody 
		rigidBody.rotation = newRotation;

		// 10. apply new velocity to rigidBody
		rigidBody.velocity = newVelocity;
	}
		
	private bool onGround () {
		return rigidBody.velocity [ 1 ] == 0.0f;
	}
}
