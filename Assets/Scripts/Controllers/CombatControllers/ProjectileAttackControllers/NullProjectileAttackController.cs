using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullProjectileAttackController : IProjectileAttackController {
	public virtual void FireProjectile ( IHealthController healthController ) {
		// do nothing
	}
	public virtual bool IsFiringProjectile () {
		return false;
	}
}
