using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectileAttackController {
	void FireProjectile ( IHealthController healthController );
	bool IsFiringProjectile ();
}
