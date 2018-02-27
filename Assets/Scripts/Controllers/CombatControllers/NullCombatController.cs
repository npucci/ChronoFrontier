using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCombatController : ICombatController {


	public void LightMeleeAttack ( IHealthController target ) {
		// do nothing
	}

	public void HeavyMeleeAttack ( IHealthController target ) {
		// do nothing
	}

	public void FireProjectile ( IHealthController target ) {
		// do nothing
	}

	public bool IsLightAttacking () {
		return false;
	}

	public bool IsHeavyAttacking () {
		return false;
	}

	public bool IsFiringProjectile () {
		return false;
	}

	public bool IsAttacking () {
		return false;
	}
}
