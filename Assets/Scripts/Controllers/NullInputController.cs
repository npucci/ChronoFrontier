using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullInputController : IInputController {

	public virtual void SetCameraController ( ICameraController cameraController ) {
		// do nothing
	}

	public virtual Vector3 GetPosition () {
		return Vector3.zero;
	}
}
