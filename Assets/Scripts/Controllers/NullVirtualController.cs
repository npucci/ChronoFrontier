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
		Vector3 forwardDirection,
		Vector3 sideDirection
	) {
		// do nothing
	}

	public virtual void RunButton (
		float xMovementStickInput,
		float yMovementStickInput,
		Vector3 forwardDirection,
		Vector3 sideDirection
	) {
		// do nothing
	}

	public virtual void Slide (
		float xMovementStickInput,
		float yMovementStickInput,
		Vector3 forwardDirection,
		Vector3 sideDirection
	) {
		// do nothing
	}

	public virtual void JumpButton () {
		// do nothing
	}

	public virtual void InteractionButton () {
		// do nothing
	}

	public virtual void MagicButton () {
		// do nothing
	}

	public virtual void TimeSlowButton () {
		// do nothing
	}

	public virtual void TimePauseButton () {
		// do nothing
	}

	public virtual void TimeStopButton () {
		// do nothing
	}

	public virtual void TimeStatusEffect ( float slowDownEffect ) {
		// do nothing
	}

	public Vector3 GetPosition () {
		return Vector3.zero;
	}
}
