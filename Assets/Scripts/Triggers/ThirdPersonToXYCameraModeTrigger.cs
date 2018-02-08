using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonToXYCameraModeTrigger : CameraModeTrigger {
	new void Start () {
		base.fromCameraMode = CameraMode.THIRD_PERSON;
		base.toCameraMode = CameraMode.SIDE_SCROLLING_XY;
		base.Start ();
	}

	public override Vector3 GetForwardDirection () {
		return Vector3.left;
	}
}
