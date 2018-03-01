using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileAttackController : MonoBehaviour , IProjectileAttackController {
	private float coolDownWaitSec;
	private float projectileSpawnDistanceFactor = 1f;
	private Timer fireCoolDownTimer;
	private Rigidbody rigidBody;

	void Start () {
		coolDownWaitSec = GetAttackCoolDownSec ();

		fireCoolDownTimer = new Timer ( coolDownWaitSec );
		rigidBody = GetComponent < Rigidbody > ();
	}

	void Update () {
		fireCoolDownTimer.updateTimer ( Time.deltaTime );
	}

	public virtual void FireProjectile ( IHealthController healthController ) {
		if ( !IsFiringProjectile () ) {
			fireCoolDownTimer.startTimer ();
			GameObject projectile = ( GameObject ) Instantiate ( Resources.Load ( GetProjectilePrefabName () ) );

			if ( projectile != null ) {
				Vector3 forwardDistance = transform.forward * projectileSpawnDistanceFactor;
				Vector3 projectileSpawnPosition = transform.position + forwardDistance;

				Rigidbody projectileRigidBody = projectile.GetComponent < Rigidbody > ();
				if ( projectileRigidBody != null ) {
					
					projectileRigidBody.position = projectileSpawnPosition;
					projectile.transform.forward = transform.forward;

					if ( rigidBody != null ) {
						projectileRigidBody.velocity = rigidBody.velocity;
					}
				}

				else {
					projectile.transform.position = projectileSpawnPosition;
					projectile.transform.forward = transform.forward;
				}
			}
		}
	}

	public virtual bool IsFiringProjectile () {
		return !fireCoolDownTimer.stopped ();
	}

	protected abstract float GetAttackCoolDownSec ();

	protected abstract string GetProjectilePrefabName ();
}

