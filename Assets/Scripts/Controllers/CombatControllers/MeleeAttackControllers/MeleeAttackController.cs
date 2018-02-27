using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeAttackController : MonoBehaviour , IMeleeAttackController {
	private float lightAttackDamage;
	private float heavyAttackDamage;
		
	private float lightAttackWindowSec;
	private Timer lightAttackWindowTimer;

	private float heavyAttackWindowSec;
	private Timer heavyAttackWindowTimer;

	void Start () {
		lightAttackDamage = GetLightAttackDamage ();
		heavyAttackDamage = GetHeavyAttackDamage (); 

		lightAttackWindowSec = GetLightAttackWindowSec ();
		lightAttackWindowTimer = new Timer ( lightAttackWindowSec );

		heavyAttackWindowSec = GetHeavyAttackWindowSec ();
		heavyAttackWindowTimer = new Timer ( heavyAttackWindowSec );
	}

	void Update () {
		
		lightAttackWindowTimer.updateTimer ( Time.deltaTime );

		heavyAttackWindowTimer.updateTimer ( Time.deltaTime );			
	}

	public virtual void LightAttack ( IHealthController healthController ) {
		if ( healthController == null ) { 
			return;
		}

		if ( !IsAttacking () ) {
			lightAttackWindowTimer.startTimer ();
			healthController.DecreaseHP ( lightAttackDamage );
		}
	}

	public virtual void HeavyAttack ( IHealthController healthController ) {
		if ( healthController == null ) { 
			return;
		}

		if ( !IsAttacking () ) {
			heavyAttackWindowTimer.startTimer ();
			healthController.DecreaseHP ( heavyAttackDamage );
		}
	}

	public virtual bool IsLightAttacking () {
		return !lightAttackWindowTimer.stopped ();
	}

	public virtual bool IsHeavyAttacking () {
		return !heavyAttackWindowTimer.stopped ();
	}

	public virtual bool IsAttacking () {
		return IsLightAttacking () || IsHeavyAttacking ();
	}

	protected abstract float GetLightAttackDamage ();

	protected abstract float GetHeavyAttackDamage ();

	protected abstract float GetLightAttackWindowSec ();

	protected abstract float GetHeavyAttackWindowSec ();
}
