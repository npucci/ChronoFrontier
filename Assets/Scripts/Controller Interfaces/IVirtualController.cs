using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVirtualController {
	void ViewStickInput ( 
		float xViewStickInput,
		float yViewStickInput
	);

	void MovementStickInput (
		float xMovementStickInput,
		float yMovementStickInput
	);

	void RunButton (
		float xMovementStickInput,
		float yMovementStickInput
	);

	void Slide (
		float xMovementStickInput,
		float yMovementStickInput
	);

	void JumpButton ();

	void InteractionButton ();

	void MagicButton ();

	void TimeSlowButton ();

	void TimePauseButton ();

	void TimeStopButton ();

}