using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour , IInputController {
	private const string INPUT_MOVEMENT_STICK_HORIZONTAL = "Horizontal";
	private const string INPUT_MOVEMENT_STICK_VERTICAL = "Vertical";

	private const string INPUT_VIEW_STICK_HORIZONTAL = "Mouse X";
	private const string INPUT_VIEW_STICK_VERTICAL = "Mouse Y";

	private const string INPUT_JUMP_BUTTON = "Jump";
	private const string INPUT_RUN_BUTTON = "Run";
	private const string INPUT_ATTACK_BUTTON = "Attack";
	private const string INPUT_RUN_TRIGGER_BUTTON = "Running";
	private const string INPUT_TIME_SLOWING_BUTTON = "Time Slowing";
	private const string INPUT_TIME_PAUSING_BUTTON = "Time Pausing";

	float horizontalMovementStickInput = 0.0f;
	float verticalMovementStickInput = 0.0f;

	float horizontalViewStickInput = 0.0f;
	float verticalViewStickInput = 0.0f;

	bool inputEnabled = true;

	private Vector3 lockPositionAxis = Vector3.zero;

	private ICameraController cameraController;

	private IVirtualController virtualController;
		
	void Start () {
		SetVirtualController ();
		cameraController = new NullCameraController ();
	}
		
	// player input
	void Update () {
		horizontalMovementStickInput = Input.GetAxis ( INPUT_MOVEMENT_STICK_HORIZONTAL );
		verticalMovementStickInput = Input.GetAxis ( INPUT_MOVEMENT_STICK_VERTICAL );

		horizontalViewStickInput += Input.GetAxis ( INPUT_VIEW_STICK_HORIZONTAL );
		verticalViewStickInput -= Input.GetAxis ( INPUT_VIEW_STICK_VERTICAL );

		if ( !inputEnabled ) {
			horizontalMovementStickInput = 0f;
			verticalMovementStickInput = 0f;

			horizontalViewStickInput = 0f;
			verticalViewStickInput = 0f;
		}
	}

	// rigidbody and physics calculations 
	void FixedUpdate () {

		cameraController.UpdateCameraPositioning (
			horizontalViewStickInput,
			verticalViewStickInput,
			GetPosition ()
		);

		if ( !inputEnabled ) {
			return;
		}
			
		if ( !GetCameraMode ().Equals ( CameraMode.THIRD_PERSON ) ) {
			verticalMovementStickInput = 0.0f;
			horizontalViewStickInput = 0.0f;
			verticalViewStickInput = 0.0f;
		}

		if ( Input.GetButtonDown ( INPUT_JUMP_BUTTON ) ) {
			virtualController.JumpButton ( true );
		}

		if ( Input.GetButtonDown ( INPUT_ATTACK_BUTTON ) ) {
			virtualController.AttackButton ( true );
		}

		//if ( Input.GetButton ( INPUT_RUN_BUTTON ) ) {
		//	virtualController.RunButton ( Input.GetButton ( INPUT_RUN_BUTTON ) );
		//}

		bool newMovementInput = horizontalMovementStickInput != 0.0f || verticalMovementStickInput != 0.0f;
		if ( newMovementInput ) {

			virtualController.MovementStickInput (
				horizontalMovementStickInput,
				verticalMovementStickInput,
				cameraController.GetCameraUpDirection (),
				cameraController.GetCameraForwardDirection (),
				cameraController.GetCameraSideDirection ()
			);

		}
	
		virtualController.TimeSlowButton ( Input.GetButton ( INPUT_TIME_SLOWING_BUTTON ) );
		virtualController.TimePauseButton ( Input.GetButton ( INPUT_TIME_PAUSING_BUTTON ) );	
	}

	private void SetVirtualController () {
		virtualController = GetComponent < IVirtualController > ();
		if ( virtualController == null ) {
			virtualController = new NullVirtualController ();
		}
	}

	public virtual void SetVirtualController ( IVirtualController virtualController ) {
		if ( virtualController == null ) {
			virtualController = new NullVirtualController ();
		}
		this.virtualController = virtualController;
	}

	public virtual void SetCameraController ( ICameraController cameraController ) {
		if ( cameraController == null ) {
			cameraController = new NullCameraController ();
		}

		cameraController.MoveCameraToTarget ( virtualController.GetPosition () );
		this.cameraController = cameraController;
	}

	public virtual void SetPosition ( Vector3 newPosition ) {
		transform.position = newPosition;
	}

	public virtual Vector3 GetPosition () {
		return virtualController.GetPosition ();
	}

	public virtual CameraMode GetCameraMode () {
		return cameraController.GetCameraMode ();
	}

	public virtual void EnableInput () {
		inputEnabled = true;
	}

	public virtual void DisableInput () {
		inputEnabled = false;
	}


	public virtual void SetCameraMode ( 
		CameraMode cameraMode, 
		Vector3 newForwardDirection 
	) {
		cameraController.SetCameraMode (
			cameraMode,
			newForwardDirection
		);
	}
}
