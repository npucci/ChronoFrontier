using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraModeTrigger {
	void SetCameraModeListener ( ICameraModeTriggerListener cameraModeTriggerListener );
	CameraMode GetFromCameraMode ();
	CameraMode GetToCameraMode ();
	Vector3 GetForwardDirection ();
}
