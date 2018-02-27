using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeManipulatorController {
	void ManipulateTime ();
	void RestoreTime ();
	bool ManipulateTimeManipulatableObject ( ITimeManipulatableController timeManipulatableController );
	bool RestoreTimeManipulatableObject ( ITimeManipulatableController timeManipulatableController );
}