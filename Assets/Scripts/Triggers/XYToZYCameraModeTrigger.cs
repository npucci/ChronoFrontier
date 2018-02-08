using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZYToXYCameraModeTrigger : CameraModeTrigger {
	new void Start () {
		base.fromCameraMode = CameraMode.SIDE_SCROLLING_ZY;
		base.toCameraMode = CameraMode.SIDE_SCROLLING_XY;
		base.Start ();
	}

	public override Vector3 GetForwardDirection () {
		return Vector3.left;
	}
}
