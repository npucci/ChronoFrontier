using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCameraController : ICameraController {
	public virtual void updateCameraPositioning ( 
		float horizontalViewStickInput,
		float verticalViewStickInput,
		Vector3 pivotPoint
	) {
		// do nothing
	}

	public virtual void MoveCameraToTarget ( Vector3 targetPosition ) {
		// do nothing
	}

	public virtual void setCameraMovementSpeed ( float cameraMovementSpeed ) {
		// do nothing
	}

	public virtual Vector3 getCameraDistanceFromTarget () {
		return Vector3.zero;
	}

	public Vector3 getCameraForwardDirection () {
		return Vector3.forward;
	}

	public Vector3 getCameraSideDirection () {
		return Vector3.right;
	}

}
