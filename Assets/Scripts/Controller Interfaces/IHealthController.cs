using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthController {
	void IncreaseHP ( float health );
	void DecreaseHP ( float damage );
	void SetMaxHP ( float maxHealthPoints );
	void SetCurrentHP ( float healthPoints );
	void MaxHP ();
	float GetMaxHP ();
	float GetCurrentHP ();
}
