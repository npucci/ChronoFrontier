using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashController : DashController {
	protected override float MaxDashSpeed () {
		return 15f;
	}

	protected override float DashSpeedIncrement () {
		return 15f;
	}
}
