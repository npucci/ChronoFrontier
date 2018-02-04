using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVirtualController {
	void MovementStickInput (
		float xMovementStickInput,
		float yMovementStickInput,
		Vector3 forwardDirection,
		Vector3 sideDirection
	);

	void RunButton (
		float xMovementStickInput,
		float yMovementStickInput,
		Vector3 forwardDirection,
		Vector3 sideDirection
	);

	void Slide (
		float xMovementStickInput,
		float yMovementStickInput,
		Vector3 forwardDirection,
		Vector3 sideDirection
	);

	void JumpButton ();

	void InteractionButton ();

	void MagicButton ();

	void TimeSlowButton ();

	void TimePauseButton ();

	void TimeStopButton ();

	void TimeStatusEffect ( float slowDownEffect );

	Vector3 GetPosition ();
}