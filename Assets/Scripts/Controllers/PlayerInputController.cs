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
	private const string INPUT_TIME_SLOWING_BUTTON = "Time Slowing";
	private const string INPUT_TIME_PAUSING_BUTTON = "Time Pausing";

	float horizontalMovementStickInput = 0.0f;
	float verticalMovementStickInput = 0.0f;

	float horizontalViewStickInput = 0.0f;
	float verticalViewStickInput = 0.0f;

	private ICameraController cameraController;
	private IVirtualController virtualController;
		
	void Start () {
		setVirtualController ();
		cameraController = new NullCameraController ();
	}
		
	// player input
	void Update () {
		horizontalMovementStickInput = Input.GetAxis ( INPUT_MOVEMENT_STICK_HORIZONTAL );
		verticalMovementStickInput = Input.GetAxis ( INPUT_MOVEMENT_STICK_VERTICAL );

		horizontalViewStickInput += Input.GetAxis ( INPUT_VIEW_STICK_HORIZONTAL );
		verticalViewStickInput -= Input.GetAxis ( INPUT_VIEW_STICK_VERTICAL );
	}

	void LateUpdate () {

	}

	// rigidbody and physics calculations 
	void FixedUpdate () {
		if ( Input.GetButtonDown ( INPUT_JUMP_BUTTON ) ) {
			virtualController.JumpButton ( true );
		}

		virtualController.RunButton ( Input.GetButton ( INPUT_RUN_BUTTON ) );

		bool newMovementInput = horizontalMovementStickInput != 0.0f || verticalMovementStickInput != 0.0f;
		if ( newMovementInput ) {
			virtualController.MovementStickInput (
				horizontalMovementStickInput,
				verticalMovementStickInput,
				cameraController.getCameraForwardDirection (),
				cameraController.getCameraSideDirection ()
			);

		}

		bool newViewStickInput = horizontalViewStickInput != 0.0f || verticalViewStickInput != 0.0f;
		if ( newViewStickInput ) {
			cameraController.updateCameraPositioning (
				horizontalViewStickInput,
				verticalViewStickInput, 
				GetPosition ()
			);
		}
			
		virtualController.TimeSlowButton ( Input.GetButton ( INPUT_TIME_SLOWING_BUTTON ) );
		virtualController.TimePauseButton ( Input.GetButton ( INPUT_TIME_PAUSING_BUTTON ) );	
	}

	private void setVirtualController () {
		virtualController = GetComponent < IVirtualController > ();
		if ( virtualController == null ) {
			virtualController = new NullVirtualController ();
		}
	}

	public virtual void SetCameraController ( ICameraController cameraController ) {
		if ( cameraController == null ) {
			cameraController = new NullCameraController ();
		}

		horizontalViewStickInput = 0.01f;
		verticalViewStickInput = 0.01f;
		this.cameraController = cameraController;
	}

	public virtual Vector3 GetPosition () {
		return virtualController.GetPosition ();
	}
}
