using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Niccolo Pucci
 * Purpose:
 * Generic depletable health, 
 * that can be used for the player and NPC/AI
*/

public class CombatHealthController : IHealthController {
	private const float EMPTY_HP = 0.0f;
	private float currentHP = 100.0f;
	private float maxHP = 100.0f;

	public CombatHealthController () : this (
		100.0f,
		100.0f
	) {
		// do nothing
	}

	public CombatHealthController (
		float currentHP,
		float maxHP
	) {
		this.currentHP = currentHP;
		this.maxHP = maxHP;
	}

	public virtual void IncreaseHP ( float health ) {
		currentHP += health;

		if ( maxHP < currentHP ) {
			currentHP = maxHP;
		}
	}

	public virtual void DecreaseHP ( float damage ) {
		currentHP -= damage;

		if ( currentHP < EMPTY_HP ) {
			currentHP = EMPTY_HP;
		}
	}

	public virtual void SetMaxHP ( float maxHealthPoints ) {
		if ( validHPValue ( maxHealthPoints ) ) {
			maxHP = maxHealthPoints;
		}
	}

	public virtual void SetCurrentHP ( float healthPoints ) {
		if ( validHPValue ( healthPoints ) ) {
			currentHP = healthPoints;
		}
	}

	public virtual void MaxHP () {
		currentHP = maxHP;
	}

	public virtual float GetMaxHP () {
		return maxHP;
	}

	public virtual float GetCurrentHP () {
		return currentHP;
	}

	private bool validHPValue ( float healthPoints ) {
		return  EMPTY_HP <= healthPoints && healthPoints <= maxHP;
	}
}