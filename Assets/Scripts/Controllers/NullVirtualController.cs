using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullVirtualController : IVirtualController {

	public virtual void ViewStickInput ( Vector3 input ) {
		// do nothing
	}

	public virtual void MovementStickInput ( Vector3 input ) {
		// do nothing
	}

	public virtual void RunButton ( Vector3 input ) {
		// do nothing
	}

	public virtual void Slide ( Vector3 input ) {
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
