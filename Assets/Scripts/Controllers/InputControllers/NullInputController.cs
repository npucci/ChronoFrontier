using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullInputController : IInputController {

	public virtual void SetCameraController ( ICameraController cameraController ) {
		// do nothing
	}

	public virtual void SetVirtualController ( IVirtualController virtualController ) {
		// do nothing
	}

	public virtual void SetPosition ( Vector3 newPosition ) {
		// do nothing
	}

	public virtual void EnableInput () {
		// do nothing
	}

	public virtual void DisableInput () {
		// do nothing
	}

	public virtual Vector3 GetPosition () {
		return Vector3.zero;
	}

	public virtual float timeEffect () {
		return 1f;
	}

	public virtual CameraMode GetCameraMode () {
		return CameraMode.NONE;
	}

	public virtual void SetCameraMode ( 		
		CameraMode cameraMode, 
		Vector3 newForwardDirection 
	) {
		// do nothing
	}
}
