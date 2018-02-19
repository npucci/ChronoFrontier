using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCameraController : ICameraController {
	
	public virtual void UpdateCameraPositioning ( 
		float horizontalViewStickInput,
		float verticalViewStickInput,
		Vector3 pivotPoint
	) {
		// do nothing
	}

	public void MoveCameraToTarget ( Vector3 targetPosition ) {
		// do nothing
	}

	public virtual void SetCameraMovementSpeed ( float cameraMovementSpeed ) {
		// do nothing
	}

	public virtual Vector3 GetCameraDistanceFromTarget () {
		return Vector3.zero;
	}

	public virtual Vector3 GetCameraUpDirection () {
		return Vector3.up;
	}

	public virtual Vector3 GetCameraForwardDirection () {
		return Vector3.forward;
	}

	public virtual Vector3 GetCameraSideDirection () {
		return Vector3.right;
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
