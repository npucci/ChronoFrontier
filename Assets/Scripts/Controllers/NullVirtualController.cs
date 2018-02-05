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

	public virtual void RunButton ( bool clicked ) {
		// do nothing
	}

	public virtual void Slide ( bool clicked ) {
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

	public virtual void TimeStatusEffect ( float slowDownEffect ) {
		// do nothing
	}

	public Vector3 GetPosition () {
		return Vector3.zero;
	}
}
