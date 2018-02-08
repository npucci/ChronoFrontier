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

	void SetRigidbodyProperties ( 
		bool useGravity, 
		bool isKinematic 
	);

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

	void TimeStatusEffect ( float slowDownEffect );

	float CurrentTimeEffect ();

	Vector3 GetPosition ();
}