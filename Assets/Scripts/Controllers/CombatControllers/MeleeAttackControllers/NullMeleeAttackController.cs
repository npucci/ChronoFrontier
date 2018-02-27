using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullMeleeAttackController : IMeleeAttackController {
	public virtual void LightAttack ( IHealthController healthController ) {
		// do nothing
	}

	public virtual void HeavyAttack ( IHealthController healthController ) {
		// do nothing
	}

	public virtual bool IsAttacking () {
		return false;
	}
}
