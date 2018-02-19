using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackController {
	void LightAttack ( IHealthController healthController );
	void HeavyAttack ( IHealthController healthController );
	bool IsAttacking ();
}