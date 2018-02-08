using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYToZYCameraModeTrigger : CameraModeTrigger {
	new void Start () {
		base.fromCameraMode = CameraMode.SIDE_SCROLLING_XY;
		base.toCameraMode = CameraMode.SIDE_SCROLLING_ZY;
		base.Start ();
	}

	public override Vector3 GetForwardDirection () {
		return Vector3.left;
	}
}
