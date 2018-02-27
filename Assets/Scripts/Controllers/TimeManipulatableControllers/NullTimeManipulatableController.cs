using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullTimeManipulatableController : ITimeManipulatableController {
	public virtual void UpdateTimeFactor ( float timeFactor ) {
		// do nothing
	}

	public virtual float TimeFactor () {
		return 1f; // return regular time scale factor
	}
}