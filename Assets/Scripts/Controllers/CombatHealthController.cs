using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Niccolo Pucci
 * Purpose:
 * Generic depletable health, 
 * that can be used for the player and NPC/AI
*/

public class CombatHealthController : MonoBehaviour , IHealthController {
	private float emptyHP;
	private float currentHP;
	private float maxHP;

	void Start () {
		emptyHP = 0.0f;
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
		return  emptyHP <= healthPoints && healthPoints <= maxHP;
	}
}