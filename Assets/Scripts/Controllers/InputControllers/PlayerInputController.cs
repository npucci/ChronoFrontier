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
	bool jumpButtonPressed = false;
	bool runButtonPressed = false;
	bool lightAttackButtonPressed = false;
	bool heavyAttackButtonPressed = false;
	bool timeSlowButtonPressed = false;
	bool timePauseButtonPressed = false;

	private Vector3 lockPositionAxis = Vector3.zero;

	private ICameraController cameraController;

	private IVirtualController virtualController;
		
	void Start () {
		SetVirtualController ();
		cameraController = new NullCameraController ();
	}
		
	// player input
	void Update () {
		if ( !inputEnabled ) {
			return;
		}

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

		if ( !GetCameraMode ().Equals ( CameraMode.THIRD_PERSON ) ) {
			verticalMovementStickInput = 0.0f;
			horizontalViewStickInput = 0.0f;
			verticalViewStickInput = 0.0f;
		}

		if ( Input.GetButtonDown ( INPUT_JUMP_BUTTON ) ) {
			jumpButtonPressed = true;
		}

		if ( Input.GetButtonDown ( INPUT_ATTACK_BUTTON ) ) {
			lightAttackButtonPressed = true;
		}

		if ( Input.GetButtonDown ( INPUT_ATTACK_BUTTON ) ) {
			heavyAttackButtonPressed = true;
		}

		if ( Input.GetButtonDown ( INPUT_RUN_BUTTON ) ) {
			runButtonPressed = true;
		}

		if ( Input.GetButtonDown ( INPUT_TIME_PAUSING_BUTTON ) ) {
			timePauseButtonPressed = true;
		}

		if ( Input.GetButtonDown ( INPUT_TIME_SLOWING_BUTTON ) ) {
			timeSlowButtonPressed = true;
		}
	}

	// update camera movement after physics and movement
	void LateUpdate () {
		cameraController.UpdateCameraPositioning (
			horizontalViewStickInput,
			verticalViewStickInput,
			GetPosition ()
		);
	}

	// rigidbody and physics calculations 
	void FixedUpdate () {

		if ( jumpButtonPressed ) {
			virtualController.JumpButton ( jumpButtonPressed );
			jumpButtonPressed = false;
		}

		if ( runButtonPressed ) {
			//virtualController.RunButton ( runButtonPressed );
			//runButtonPressed = false;
		}

		if ( lightAttackButtonPressed ) {
			virtualController.AttackButton ( lightAttackButtonPressed );
			lightAttackButtonPressed = false;
		}

		if ( heavyAttackButtonPressed ) {
			virtualController.AttackButton ( heavyAttackButtonPressed );
			heavyAttackButtonPressed = false;
		}
			

		if ( timePauseButtonPressed ) {
			virtualController.TimePauseButton ( timePauseButtonPressed );
			timePauseButtonPressed = false;
		}
			

		if ( timeSlowButtonPressed ) {
			virtualController.TimeSlowButton ( timeSlowButtonPressed );
			timeSlowButtonPressed = false;
		}

		virtualController.MovementStickInput (
			horizontalMovementStickInput,
			verticalMovementStickInput,
			cameraController.GetCameraUpDirection (),
			cameraController.GetCameraForwardDirection (),
			cameraController.GetCameraSideDirection ()
		);
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
