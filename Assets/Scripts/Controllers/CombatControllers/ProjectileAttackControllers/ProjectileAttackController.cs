using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileAttackController : MonoBehaviour , IProjectileAttackController {
	private float coolDownWaitSec;
	private Timer fireCoolDownTimer;

	void Start () {
		coolDownWaitSec = GetAttackCoolDownSec ();

		fireCoolDownTimer = new Timer ( coolDownWaitSec );
	}

	void Update () {
		fireCoolDownTimer.updateTimer ( Time.deltaTime );
	}

	public virtual void FireProjectile ( IHealthController healthController ) {
		Debug.Log ( "Enter FireProjectile ()" );

		if ( !IsFiringProjectile () ) {
			Debug.Log ( "firing" );
			fireCoolDownTimer.startTimer ();
			GameObject projectile = ( GameObject ) Instantiate ( Resources.Load ( GetProjectilePrefabName () ) );
			if ( projectile != null ) {
				Debug.Log ( "Instantiated" );
				projectile.transform.position = transform.position;
				projectile.transform.up = transform.forward;
			}
		}
	}

	public virtual bool IsFiringProjectile () {
		return !fireCoolDownTimer.stopped ();
	}

	protected abstract float GetAttackCoolDownSec ();

	protected abstract string GetProjectilePrefabName ();
}

