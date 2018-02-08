using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZYModeRestrictor : ModeRestrictor {
	new void Start () {
		base.cameraMode = CameraMode.SIDE_SCROLLING_ZY;
		base.Start ();
	}
}
