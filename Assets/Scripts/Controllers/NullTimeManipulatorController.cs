using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullTimeManipulatorController : ITimeManipulatorController {
	public virtual void TimeSlow () {
		// do nothing
	}

	public virtual void TimePause () {
		// do nothing
	}

	public virtual void TimeStop () {
		// do nothing
	}

	public virtual void TimeRestore () {
		// do nothing
	}

	public virtual void AddTimeManipulatableObject ( IVirtualController virtualController ) {
		// do nothing
	}

	public virtual void RemoveTimeManipulatableObject ( IVirtualController virtualController ) {
		// do nothing
	}
}
