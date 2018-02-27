using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTimeManipulatorController : TimeManipulatorController {
	protected override float TimeManipulationFactor () {
		return 0.15f;
	}

	protected override float TimeManipulationSphereScale () {
		return 3f;
	}

	protected override string TimeManipulationSphereMaterial () {
		return "Materials/TimeFieldSlow";	
	}
}
