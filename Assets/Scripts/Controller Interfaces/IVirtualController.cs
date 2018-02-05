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

	void RunButton ( bool clicked );

	void Slide ( bool clicked );

	void JumpButton ( bool clicked );

	void InteractionButton ( bool clicked );

	void MagicButton ( bool clicked );

	void TimeSlowButton ( bool clicked );

	void TimePauseButton ( bool clicked );

	void TimeStopButton ( bool clicked );

	void TimeStatusEffect ( float slowDownEffect );

	Vector3 GetPosition ();
}