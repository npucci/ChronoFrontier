using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeManipulatorController {
	void TimeSlow ();
	void TimePause ();
	void TimeStop ();
	void TimeRestore ();
	void AddTimeManipulatableObject ( IVirtualController virtualController );
	void RemoveTimeManipulatableObject ( IVirtualController virtualController );
}