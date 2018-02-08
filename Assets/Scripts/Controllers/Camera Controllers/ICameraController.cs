using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraController {

	void UpdateCameraPositioning ( 
		float horizontalViewStickInput,
		float verticalViewStickInput,
		Vector3 pivotPoint
	);

	void MoveCameraToTarget ( Vector3 targetPosition );

	void SetCameraMovementSpeed ( float cameraMovementSpeed );

	Vector3 GetCameraDistanceFromTarget ();

	Vector3 GetCameraForwardDirection ();

	Vector3 GetCameraSideDirection ();

	CameraMode GetCameraMode ();

	void SetCameraMode ( 
		CameraMode cameraMode, 
		Vector3 newForwardDirection
	);
}