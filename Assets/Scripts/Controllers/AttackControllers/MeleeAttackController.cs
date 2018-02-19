using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeAttackController : MonoBehaviour , IAttackController {
	private float lightAttackDamage;
	private float heavyAttackDamage;
		
	private float attackWaitSec;
	private float attackWindowSec;
	private Timer attackWaitTimer;
	private Timer attackWindowTimer;

	void Start () {
		lightAttackDamage = GetLightAttackDamage ();
		heavyAttackDamage = GetHeavyAttackDamage (); 

		attackWaitSec = GetAttackCoolDownSec ();
		attackWindowSec = GetAttackWindowSec ();

		attackWaitTimer = new Timer ( attackWaitSec );
		attackWindowTimer = new Timer ( attackWindowSec );
	}

	void Update () {
		attackWaitTimer.updateTimer ( Time.deltaTime );
		attackWindowTimer.updateTimer ( Time.deltaTime );
	}

	public virtual void LightAttack ( IHealthController healthController ) {
		Attack ( 
			healthController,
			lightAttackDamage
		);
	}

	public virtual void HeavyAttack ( IHealthController healthController ) {
		Attack ( 
			healthController,
			heavyAttackDamage
		);
	}

	public virtual bool IsAttacking () {
		return attackWaitTimer.stopped ();
	}

	private void Attack (
		IHealthController healthController, 
		float attackDamage 
	) {
		if ( !IsAttacking () ) {
			attackWaitTimer.startTimer ();
			attackWindowTimer.startTimer ();
			healthController.DecreaseHP ( attackDamage );
		}
	}

	protected abstract float GetLightAttackDamage ();

	protected abstract float GetHeavyAttackDamage ();

	protected abstract float GetAttackCoolDownSec ();

	protected abstract float GetAttackWindowSec ();
}
