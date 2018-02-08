using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraModeTriggerListener {
	void OnCameraModeTrigger ( 
		CameraMode cameraMode, 
		Vector3 newForwardDirection 
	);
}
