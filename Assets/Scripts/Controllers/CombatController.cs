using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour , ICombatController {
	private float attackDamage;
		
	void Start () {
		attackDamage = 0.0f;
	}

	public virtual float Attack () {
		return attackDamage;
	}

	public virtual void SetAttackDamage ( float attackDamage ) {
		this.attackDamage = Mathf.Abs ( attackDamage );
	}
}
