using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Niccolo Pucci
 * Purpose:
 * Generic depletable health, 
 * that can be used for the player and NPC/AI
*/

public abstract class HealthController : MonoBehaviour , IHealthController {
	private float emptyHP = 0f;
	private float currentHP;
	private float maxHP;

	protected void Start () {
		currentHP = 100.0f;
		maxHP = 100.0f;
	}

	public virtual void IncreaseHP ( float health ) {
		currentHP += health;

		if ( maxHP < currentHP ) {
			currentHP = maxHP;
		}
	}

	public virtual void DecreaseHP ( float damage ) {
		currentHP -= damage;
		if ( currentHP < emptyHP ) {
			currentHP = emptyHP;
			Destroy ( gameObject );
		}
	}

	public virtual void SetCurrentHP ( float healthPoints ) {
		if ( validHPValue ( healthPoints ) ) {
			currentHP = healthPoints;
		}
	}

	public virtual void SetToMaxHP () {
		currentHP = maxHP;
	}

	public abstract float GetMaxHP ();

	public virtual float GetCurrentHP () {
		return currentHP;
	}

	private bool validHPValue ( float healthPoints ) {
		return  emptyHP <= healthPoints && healthPoints <= maxHP;
	}
}