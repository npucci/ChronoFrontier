using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeManipulatorController {
	void TimeSlow ();
	void TimePause ();
	void TimeStop ();
	void TimeRestore ();
	void AddTimeManipulatableObject ( BodyVirtualController virtualController );
	void RemoveTimeManipulatableObject ( BodyVirtualController virtualController );
}