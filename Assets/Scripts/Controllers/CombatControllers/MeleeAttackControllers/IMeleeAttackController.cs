using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeAttackController {
	void LightAttack ( IHealthController healthController );
	void HeavyAttack ( IHealthController healthController );
	bool IsAttacking ();
}
