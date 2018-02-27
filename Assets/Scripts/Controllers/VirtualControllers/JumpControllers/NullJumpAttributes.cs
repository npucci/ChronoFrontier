using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullJumpAttributes : IJumpController {
	public bool Jump ( Rigidbody rigidBody ) {
		return false;
	}
}
