using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputController {
	void SetCameraController ( ICameraController cameraController );
	void SetVirtualController ( IVirtualController virtualController );
	void SetPosition ( Vector3 newPosition );
	void EnableInput ();
	void DisableInput ();
	Vector3 GetPosition ();
	CameraMode GetCameraMode ();
	void SetCameraMode ( 
		CameraMode cameraMode, 
		Vector3 newForwardDirection
	);
}