using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullTimeManipulatorController : ITimeManipulatorController {
	public virtual void ManipulateTime () {
		// do nothing
	}

	public virtual void RestoreTime () {
		// do nothing
	}

	public virtual bool ManipulateTimeManipulatableObject ( ITimeManipulatableController virtualController ) {
		return false;
	}

	public virtual bool RestoreTimeManipulatableObject ( ITimeManipulatableController virtualController ) {
		return false;
	}
}
