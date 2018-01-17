﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCameraController : ICameraController {

	public NullCameraController () {
		// do nothing
	}

	public virtual void updateCameraPositioning ( 
		float horizontalViewStickInput,
		float verticalViewStickInput,
		Vector3 pivotPoint
	) {
		// do nothing
	}


	public virtual void setCameraMovementSpeed ( float cameraMovementSpeed ) {
		// do nothing
	}

	public virtual Vector3 getCameraForwardVector () {
		return Vector3.zero;
	}


	public virtual Vector3 getCameraDistanceFromTarget () {
		return Vector3.zero;
	}

	public virtual void setCamera ( Camera Camera ) {
		// do nothing
	}
		
	public virtual bool hasCamera () {
		return false;
	}

	public Vector3 getCameraForwardDirection () {
		return Vector3.zero;
	}

	public Vector3 getCameraSideDirection () {
		return Vector3.zero;
	}

}