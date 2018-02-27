using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowProjectileAttackController : ProjectileAttackController {

	protected override float GetAttackCoolDownSec () {
		return 1f;
	}

	protected override string GetProjectilePrefabName () {
		return "Prefabs/Projectiles/Time Slow Projectile";
	}
}
