using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeManipulatorController {
	float TimeSlow ();
	float TimePause ();
	float TimeRestore ();
}