using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVirtualController {
	void ViewStickInput ( Vector3 input );

	void MovementStickInput ( Vector3 input );

	void RunButton ( Vector3 input );

	void Slide ( Vector3 input );

	void JumpButton ();

	void InteractionButton ();

	void MagicButton ();

	void TimeSlowButton ();

	void TimePauseButton ();

	void TimeStopButton ();

}