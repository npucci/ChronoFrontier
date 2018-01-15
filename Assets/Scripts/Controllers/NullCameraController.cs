using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCameraController : ICameraController {

	public NullCameraController () {
		// do nothing
	}

	public virtual void updateCameraPositioning ( 
		Vector3 input,
		Vector3 pivotPoint
	) {
		// do nothing
	}


	public virtual void setCameraMovementSpeed ( float cameraMovementSpeed ) {
		// do nothing
	}

	public virtual Quaternion getCameraRotation () {
		return new Quaternion (
			0,
			0,
			0,
			0
		);
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


}
