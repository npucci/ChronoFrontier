using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {
	private const string INPUT_MOVEMENT_STICK_HORIZONTAL = "Horizontal";
	private const string INPUT_MOVEMENT_STICK_VERTICAL = "Vertical";

	private const string INPUT_VIEW_STICK_HORIZONTAL = "Mouse X";
	private const string INPUT_VIEW_STICK_VERTICAL = "Mouse Y";

	private const string INPUT_JUMP_BUTTON = "Jump";
	private const string INPUT_RUN_BUTTON = "Run";

	float horizontalMovementStickInput = 0.0f;
	float verticalMovementStickInput = 0.0f;

	float horizontalViewStickInput = 0.0f;
	float verticalViewStickInput = 0.0f;

	private IVirtualController virtualController;
		
	void Start () {
		virtualController = GetComponent < IVirtualController > ();
		if ( virtualController == null ) {
			virtualController = new NullVirtualController ();
		}
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
		bool newMovementInput = horizontalMovementStickInput != 0.0f || verticalMovementStickInput != 0.0f;
		if ( newMovementInput ) {
			if ( Input.GetButton ( INPUT_RUN_BUTTON ) ) {
				Debug.Log ( "running button" );
				virtualController.RunButton (
					horizontalMovementStickInput,
					verticalMovementStickInput
				);
			} 

			else {
				virtualController.MovementStickInput (
					horizontalMovementStickInput,
					verticalMovementStickInput
				);
			}
		}

		virtualController.ViewStickInput (
			horizontalViewStickInput, 
			verticalViewStickInput
		);

		if ( Input.GetButtonDown ( INPUT_JUMP_BUTTON ) ) {
			virtualController.JumpButton ();
		}
			
	}






}
