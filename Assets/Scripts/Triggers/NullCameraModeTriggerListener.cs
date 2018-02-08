using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCameraModeTriggerListener : ICameraModeTriggerListener {

	public virtual void OnCameraModeTrigger ( 
		CameraMode cameraMode, 
		Vector3 newForwardDirection 
	) {
		// do nothing
	}

	public virtual Vector3 GetForwardDirection () {
		return Vector3.zero;
	}
}
