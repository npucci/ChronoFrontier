using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatController {
	void LightMeleeAttack ( IHealthController target );
	void HeavyMeleeAttack ( IHealthController target );
	void FireProjectile ( IHealthController target );
	bool IsLightAttacking ();
	bool IsHeavyAttacking ();
	bool IsFiringProjectile ();
	bool IsAttacking ();
}
