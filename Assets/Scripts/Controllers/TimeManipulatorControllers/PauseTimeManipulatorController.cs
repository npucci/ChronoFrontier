using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTimeManipulatorController : TimeManipulatorController {
	protected override float TimeManipulationFactor () {
		return 0f;
	}

	protected override float TimeManipulationSphereScale () {
		return 3f;
	}

	protected override string TimeManipulationSphereMaterial () {
		return "Materials/TimeFieldPause";	
	}

}
