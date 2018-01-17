using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : ICameraController {
	private Camera camera;
	private float cameraMovementSpeed;
	private Vector3 cameraAngleFromTarget;

	private Vector3 cameraDistanceFromTarget;

	public ThirdPersonCameraController ( float movementSpeed ) {
		setCameraAngleFromTarget ( new Vector3 (
			15.0f,
			0.0f,
			0.0f
		) );

		setCameraDistanceFromTarget ( new Vector3 (
			0.0f,
			0.0f,
			-5.0f
		) );

		setCamera ( Camera.main );
		setCameraMovementSpeed ( movementSpeed );
	}

	public virtual void updateCameraPositioning ( 
		float horizontalViewStickInput,
		float verticalViewStickInput,
		Vector3 pivotPoint
	) {
		if ( camera == null ) {
			return;
		}
			
		Vector3 desiredRotation = new Vector3 (
			verticalViewStickInput * cameraMovementSpeed,
			horizontalViewStickInput * cameraMovementSpeed,
			0.0f
		);

		Quaternion newRotation = Quaternion.Slerp (
			camera.transform.rotation,
			Quaternion.Euler ( desiredRotation ),
			cameraMovementSpeed
		);

		//newRotation = Quaternion.Euler ( desiredRotation );
		camera.transform.rotation = newRotation;

		Vector3 newPosition = pivotPoint + (newRotation * cameraDistanceFromTarget );
		camera.transform.position = newPosition;
	}

	public virtual void setCameraMovementSpeed ( float cameraMovementSpeed ) {
		this.cameraMovementSpeed = cameraMovementSpeed;
	}

	public virtual Vector3 getCameraForwardVector () {
		Vector3 forwardDirection = camera.transform.forward;
		//Vector3 forwardDirection = camera.transform.rotation.eulerAngles;
		return forwardDirection;
	}

	public virtual Vector3 getCameraDistanceFromTarget () {
		return cameraDistanceFromTarget;
	}

	public virtual Vector3 getCameraAngleFromTarget () {
		return cameraAngleFromTarget;
	}

	public virtual void setCamera ( Camera camera ) {
		if ( camera == null ) {
			camera = new Camera ();
		}

		this.camera = camera;
	}

	public virtual bool hasCamera () {
		return  camera != null;
	}

	private void setCameraDistanceFromTarget ( Vector3 cameraDistanceFromTarget ) {
		this.cameraDistanceFromTarget = cameraDistanceFromTarget;
	}

	private void setCameraAngleFromTarget ( Vector3 angleFromTarget ) {

	}

	public Vector3 getCameraForwardDirection () {
		Vector3 cameraForwardDirection = camera.transform.forward;
		cameraForwardDirection.y = 0.0f;
		// return only x and z axis information
		return cameraForwardDirection;
	}

	public Vector3 getCameraSideDirection () {
		Vector3 cameraSideDirection = camera.transform.right;
		cameraSideDirection.y = 0.0f;
		// return only x and z axis information
		return cameraSideDirection;
	}

}