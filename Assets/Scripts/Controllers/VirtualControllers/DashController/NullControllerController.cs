using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullDashController : IDashController {
	public virtual void Dash ( bool input ) {
		// do nothing
	}

	public virtual float DashSpeed () {
		return 0f;
	}
}
