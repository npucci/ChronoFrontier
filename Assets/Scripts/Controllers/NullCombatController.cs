using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCombatController : ICombatController {
	public virtual float Attack () {
		return 0.0f;
	}

	public virtual void SetAttackDamage ( float attackDamage ) {
		// do nothing
	}
}