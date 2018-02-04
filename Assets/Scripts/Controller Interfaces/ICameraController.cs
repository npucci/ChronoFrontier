using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraController {

	void updateCameraPositioning ( 
		float horizontalViewStickInput,
		float verticalViewStickInput,
		Vector3 pivotPoint
	);

	void setCameraMovementSpeed ( float cameraMovementSpeed );

	void MoveCameraToTarget ( Vector3 targetPosition );

	Vector3 getCameraDistanceFromTarget ();

	Vector3 getCameraForwardDirection ();

	Vector3 getCameraSideDirection ();
}
