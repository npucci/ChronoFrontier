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

	protected override float GetAttackCoolDownSec () {
		return 0.2f;
	}

	protected override float GetAttackWindowSec () {
		return 0.1f;
	}
}
