using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {
	private const string INPUT_AXIS_HORIZONTAL = "Horizontal";
	private const string INPUT_AXIS_VERTICAL = "Vertical";
	private const string INPUT_MOUSE_X = "Mouse X";
	private const string INPUT_MOUSE_Y = "Mouse Y";
	private const string INPUT_JUMP_BUTTON = "Jump";

	float xMovementAxisInput = 0.0f;
	float zMovementAxisInput = 0.0f;
	float xMouseInput = 0.0f;
	float yMouseInput = 0.0f;

	private IVirtualController virtualController;
		
	void Start () {
		
		virtualController = GetComponent < IVirtualController > ();
		if ( virtualController == null ) {
			virtualController = new NullVirtualController ();
		}
	}
		
	// player input
	void Update () {
		xMovementAxisInput = Input.GetAxis ( INPUT_AXIS_HORIZONTAL );
		zMovementAxisInput = Input.GetAxis ( INPUT_AXIS_VERTICAL );

		xMouseInput += Input.GetAxis ( INPUT_MOUSE_X );
		yMouseInput -= Input.GetAxis ( INPUT_MOUSE_Y );
	}

	void LateUpdate () {

	}

	// rigidbody and physics calculations 
	void FixedUpdate () {
		Vector3 input = new Vector3 (
			xMovementAxisInput,
			0.0f,
			zMovementAxisInput
		);
			
		bool hasInput = xMovementAxisInput != 0.0f || zMovementAxisInput != 0.0f; 

		if ( hasInput ) {
			virtualController.MovementStickInput ( input );
		}

		virtualController.ViewStickInput ( new Vector3 (
			yMouseInput, 
			xMouseInput,
			0.0f
		) );

		if ( Input.GetButtonDown ( INPUT_JUMP_BUTTON ) ) {
			virtualController.JumpButton ();
		}
	}






}
