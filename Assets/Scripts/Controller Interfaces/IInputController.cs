using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputController {

	void SetCameraController ( ICameraController cameraController );

	Vector3 GetPosition ();
}
