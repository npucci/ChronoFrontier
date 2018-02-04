using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullTimeManipulatorController : ITimeManipulatorController {
	private float negligibleTimeFactor = 1.0f;

	public virtual float TimeSlow () {
		return negligibleTimeFactor;
	}

	public virtual float TimePause () {
		return negligibleTimeFactor;
	}

	public virtual float TimeRestore () {
		return negligibleTimeFactor;
	}
}
