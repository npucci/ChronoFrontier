using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDashController {
	void Dash ( bool input );
	float DashSpeed ();
}
