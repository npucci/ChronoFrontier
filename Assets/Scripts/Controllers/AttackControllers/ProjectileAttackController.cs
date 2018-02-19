using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileAttackController : MonoBehaviour , IAttackController {
	private float attackWaitSec;
	private Timer attackWaitTimer;

	void Start () {
		attackWaitSec = GetAttackCoolDownSec ();

		attackWaitTimer = new Timer ( attackWaitSec );
	}

	void Update () {
		attackWaitTimer.updateTimer ( Time.deltaTime );
	}

	public virtual void LightAttack ( IHealthController healthController ) {
		Attack ( healthController );
	}

	public virtual void HeavyAttack ( IHealthController healthController ) {
		Attack ( healthController );
	}

	public virtual bool IsAttacking () {
		return attackWaitTimer.stopped ();
	}

	private void Attack ( IHealthController healthController ) {
		if ( !IsAttacking () ) {
			attackWaitTimer.startTimer ();
			healthController.DecreaseHP ( GetProjectileAttackDamage () );

			GameObject projectile = ( GameObject ) Instantiate ( Resources.Load ( GetProjectilePrefabName () ) );
			if ( projectile != null ) {
				projectile.transform.position = transform.position;
				projectile.transform.up = transform.forward;
			}
		}
	}

	protected abstract float GetProjectileAttackDamage ();

	protected abstract float GetAttackCoolDownSec ();

	protected abstract string GetProjectilePrefabName ();
}

