using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHealthController : HealthController {

	public override float GetMaxHP ( ) {
		return 10f;	
	}
}