using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILifeTimeController {
	void StartLifeTime ();
	float TimeRemainingSec ();
	float TimeRemainingDecimal ();
	bool LifeTimeEnded ();
	void SetLifeTimeListener ( ILifeTimeListener lifeTimeListener );
}
