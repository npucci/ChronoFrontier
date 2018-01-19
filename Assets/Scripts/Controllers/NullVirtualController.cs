using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullVirtualController : IVirtualController {

	public virtual void ViewStickInput ( 
		float xViewStickInput,
		float yViewStickInput
	) {
		// do nothing
	}

	public virtual void MovementStickInput (
		float xMovementStickInput,
		float yMovementStickInput
	) {
		// do nothing
	}

	public virtual void RunButton (
		float xMovementStickInput,
		float yMovementStickInput
	) {
		// do nothing
	}

	public virtual void Slide (
		float xMovementStickInput,
		float yMovementStickInput
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
}
