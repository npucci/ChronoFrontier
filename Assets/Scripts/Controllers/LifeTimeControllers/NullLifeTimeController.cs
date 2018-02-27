using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullLifeTimeController : ILifeTimeController {
	public virtual void StartLifeTime () {
		// do nothing
	}

	public virtual float TimeRemainingSec () {
		return 0f;
	}

	public virtual float TimeRemainingDecimal () {
		return 0f;
	}

	public virtual bool LifeTimeEnded () {
		return false;
	}

	public virtual void SetLifeTimeListener ( ILifeTimeListener lifeTimeListener ) {
		// do nothing
	}
}
