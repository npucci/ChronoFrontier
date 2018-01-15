using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraController {

	void updateCameraPositioning ( 
		Vector3 input,
		Vector3 pivotPoint
	);

	void setCameraMovementSpeed ( float cameraMovementSpeed );

	Quaternion getCameraRotation ();

	Vector3 getCameraDistanceFromTarget ();

	void setCamera ( Camera Camera );

	bool hasCamera ();
}
