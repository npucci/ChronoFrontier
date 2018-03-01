using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullVirtualController : IVirtualController {

	public virtual void SetCameraController ( ICameraController cameraController ) {
		// do nothing
	}

	public virtual void MovementStickInput (
		float xMovementStickInput,
		float yMovementStickInput,
		Vector3 upDirection,
		Vector3 forwardDirection,
		Vector3 sideDirection
	) {
		// do nothing
	}

	public virtual void SetMovementSpeedProperties ( 
		float movementSpeed, 
		float runningSpeed, 
		float turnSpeed,
		float jumpingSpeed
	) {
		// do nothing
	}

	public virtual void SetRigidbodyProperties () {
		// do nothing
	}

	public virtual void DesiredRotation ( Quaternion desiredRotation ) {
		// do nothing
	}

	public virtual void RunButton ( bool clicked ) {
		// do nothing
	}

	public virtual void AttackButton ( bool clicked ) {
		// do nothing
	}

	public virtual void BoostButton ( bool clicked ) {
		// do nothing
	}

	public virtual void JumpButton ( bool clicked ) {
		// do nothing
	}

	public virtual void InteractionButton ( bool clicked ) {
		// do nothing
	}

	public virtual void MagicButton ( bool clicked ) {
		// do nothing
	}

	public virtual void TimeSlowButton ( bool clicked ) {
		// do nothing
	}

	public virtual void TimePauseButton ( bool clicked ) {
		// do nothing
	}

	public virtual void TimeStopButton ( bool clicked ) {
		// do nothing
	}

	public Vector3 GetPosition () {
		return Vector3.zero;
	}
}
