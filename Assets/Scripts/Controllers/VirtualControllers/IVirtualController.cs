using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVirtualController {
	void MovementStickInput (
		float xMovementStickInput,
		float yMovementStickInput,
		Vector3 upDirection,
		Vector3 forwardDirection,
		Vector3 sideDirection
	);

	void SetRigidbodyProperties ();

	void SetMovementSpeedProperties ( 
		float movementSpeed, 
		float runningSpeed, 
		float turnSpeed,
		float jumpingSpeed
	);

	void DesiredRotation ( Quaternion desiredRotation );

	void RunButton ( bool clicked );

	void Slide ( bool clicked );

	void JumpButton ( bool clicked );

	void InteractionButton ( bool clicked );

	void AttackButton ( bool clicked );

	void MagicButton ( bool clicked );

	void TimeSlowButton ( bool clicked );

	void TimePauseButton ( bool clicked );

	void TimeStopButton ( bool clicked );

	Vector3 GetPosition ();
}