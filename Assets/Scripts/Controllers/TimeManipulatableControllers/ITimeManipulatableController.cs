using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeManipulatableController {
	void UpdateTimeFactor ( float timeFactor );
	float TimeFactor ();
}
