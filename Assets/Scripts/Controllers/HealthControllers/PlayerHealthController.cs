using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : HealthController {

	public override float GetMaxHP ( ) {
		return 100f;	
	}
}
