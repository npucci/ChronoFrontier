using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYModeRestrictor : ModeRestrictor {
	new void Start () {
		base.cameraMode = CameraMode.SIDE_SCROLLING_XY;
		base.Start ();
	}
}
