using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePauseProjectileAttackController : ProjectileAttackController {

	protected override float GetAttackCoolDownSec () {
		return 1f;
	}

	protected override string GetProjectilePrefabName () {
		return "Prefabs/Projectiles/Time Pause Projectile";
	}
}
