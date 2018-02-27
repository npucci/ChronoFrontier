using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MeleeAttackController {

	protected override float GetLightAttackDamage () {
		return 15f;
	}

	protected override float GetHeavyAttackDamage () {
		return 20f;
	}

	protected override float GetLightAttackWindowSec () {
		return 0.1f;
	}

	protected override float GetHeavyAttackWindowSec () {
		return 0.2f;
	}
}
