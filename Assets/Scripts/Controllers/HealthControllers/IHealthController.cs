using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthController {
	void IncreaseHP ( float health );
	void DecreaseHP ( float damage );
	void SetCurrentHP ( float healthPoints );
	void SetToMaxHP ();
	float GetMaxHP ();
	float GetCurrentHP ();
}
