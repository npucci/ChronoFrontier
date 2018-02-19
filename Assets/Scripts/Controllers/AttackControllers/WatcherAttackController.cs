using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherAttackController : ProjectileAttackController {

	protected override float GetProjectileAttackDamage () {
		return 15f;
	}

	protected override float GetAttackCoolDownSec () {
		return 0.2f;
	}

	protected override string GetProjectilePrefabName () {
		return "Prefabs/Prototype 1/Projectile";
	}
}
