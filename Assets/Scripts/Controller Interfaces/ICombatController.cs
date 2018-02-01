using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatController {
	float Attack ();
	void SetAttackDamage ( float attackDamage );
}