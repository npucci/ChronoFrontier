using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInputController : MonoBehaviour , IInputController {
	private IVirtualController virtualController;
	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		virtualController = GetComponent < IVirtualController > ();
		if ( virtualController == null ) {
			virtualController = new NullVirtualController ();
		}
	}

	void Update () {
		virtualController.RunButton ( true );
		virtualController.MovementStickInput (
			0f,
			10f,
			transform.up,
			transform.forward,
			transform.right
		);
			
	}

	public virtual void SetCameraController ( ICameraController cameraController ) {
		// do nothing
	}

	public virtual void SetVirtualController ( IVirtualController virtualController ) {
		// do nothing
	}

	public virtual void SetPosition ( Vector3 newPosition ) {
		// do nothing
	}

	public virtual void EnableInput () {
		// do nothing
	}

	public virtual void DisableInput () {
		// do nothing
	}

	public virtual Vector3 GetPosition () {
		return virtualController.GetPosition ();
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
