using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour , ICombatController {
	private IMeleeAttackController meleeAttackController;
	private ProjectileAttackController projectileAttackController;

	// Use this for initialization
	void Start () {
		meleeAttackController = GetComponent < IMeleeAttackController > ();
		if ( meleeAttackController == null ) {
			meleeAttackController = new NullMeleeAttackController ();
		}

		projectileAttackController = GetComponent < ProjectileAttackController > ();
		if ( projectileAttackController == null ) {
			//projectileAttackController = new NullProjectileAttackController ();
		}
	}

	public void LightMeleeAttack ( IHealthController target ) {
		meleeAttackController.LightAttack ( target );
	}

	public void HeavyMeleeAttack ( IHealthController target ) {
		meleeAttackController.HeavyAttack ( target );
	}

	public void FireProjectile ( IHealthController target ) {
		projectileAttackController.FireProjectile ( target );
	}

	public bool IsLightAttacking () {
		return meleeAttackController.IsAttacking ();
	}

	public bool IsHeavyAttacking () {
		return meleeAttackController.IsAttacking ();
	}

	public bool IsFiringProjectile () {
		return projectileAttackController.IsFiringProjectile ();
	}

	public bool IsAttacking () {
		return meleeAttackController.IsAttacking () || projectileAttackController.IsFiringProjectile ();
	}
}
