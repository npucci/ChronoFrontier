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

	Vector3 getCameraForwardVector ();

	Vector3 getCameraDistanceFromTarget ();

	void setCamera ( Camera Camera );

	Vector3 getCameraForwardDirection ();

	Vector3 getCameraSideDirection ();

	bool hasCamera ();
}
