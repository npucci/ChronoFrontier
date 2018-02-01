using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullHealthController : MonoBehaviour , IHealthController {
	public virtual void IncreaseHP ( float health ) {
		// do nothing
	}

	public virtual void DecreaseHP ( float damage ) {
		// do nothing
	}

	public virtual void SetMaxHP ( float maxHealthPoints ) {
		// do nothing
	}

	public virtual void SetCurrentHP ( float healthPoints ) {
		// do nothing
	}

	public virtual void MaxHP () {
		// do nothing
	}

	public virtual float GetMaxHP () {
		return 0.0f;
	}

	public virtual float GetCurrentHP () {
		return 0.0f;
	}
}