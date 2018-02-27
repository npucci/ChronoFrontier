using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCameraController : MonoBehaviour , ICameraController {
	private CameraMode cameraMode = CameraMode.THIRD_PERSON;
	private float cameraMovementSpeed = 6.5f;
	private Transform targetTransform = null;

	private Vector3 cameraAngleFromTarget = Vector3.zero;
	private Vector3 cameraDistanceFromTarget = Vector3.zero;

	private Vector3 thirdPersonCameraAngleFromTarget = new Vector3 (
		15.0f,
		0.0f,
		0.0f
	);
	private Vector3 thirdPersonCameraDistanceFromTarget = new Vector3 (
		0.0f,
		0.0f,
		-6.0f
	);

	private Vector3 xySideScrollingCameraAngleFromTarget = new Vector3 (
		10.0f,
		0.0f,
		0.0f
	);
	private Vector3 xySsideScrollingCameraDistanceFromTarget = new Vector3 (
		0.0f,
		0.0f,
		-10.0f
	);

	private Vector3 zySideScrollingCameraAngleFromTarget = new Vector3 (
		10.0f,
		0.0f,
		0.0f
	);
	private Vector3 zySideScrollingCameraDistanceFromTarget = new Vector3 (
		0.0f,
		0.0f,
		-10.0f
	);

	private Vector3 thirdPersoncameraDistanceFromTarget;

	void Start () {
		SetColliderAndRigidbody ();

		cameraAngleFromTarget = thirdPersonCameraAngleFromTarget;
		cameraDistanceFromTarget = thirdPersonCameraDistanceFromTarget;

		SetCameraAngleFromTarget ( cameraAngleFromTarget );
		SetCameraDistanceFromTarget ( cameraDistanceFromTarget );
	}

	public virtual void MoveCameraToTarget ( Vector3 targetPosition ) {
		UpdateCameraPositioning (
			100.0f,
			100.0f,
			targetPosition
		);
	}

	public virtual void SetCameraMode (
		CameraMode cameraMode, 
		Vector3 newForwardDirection 
	) {
		this.cameraMode = cameraMode;

		if ( cameraMode.Equals ( CameraMode.THIRD_PERSON ) ) {
			cameraAngleFromTarget = thirdPersonCameraAngleFromTarget;
			cameraDistanceFromTarget = thirdPersonCameraDistanceFromTarget;
		}

		else if ( cameraMode.Equals ( CameraMode.SIDE_SCROLLING_XY ) ) {
			cameraAngleFromTarget = xySideScrollingCameraAngleFromTarget;
			cameraDistanceFromTarget = xySsideScrollingCameraDistanceFromTarget;
		}

		else if ( cameraMode.Equals ( CameraMode.SIDE_SCROLLING_ZY ) ) {
			cameraAngleFromTarget = zySideScrollingCameraAngleFromTarget;
			cameraDistanceFromTarget = zySideScrollingCameraDistanceFromTarget;
		}

		SetCameraAngleFromTarget ( cameraAngleFromTarget );
		SetCameraDistanceFromTarget ( cameraDistanceFromTarget );
	}

	public virtual void UpdateCameraPositioning ( 
		float horizontalViewStickInput,
		float verticalViewStickInput,
		Vector3 pivotPoint
	) {
		Vector3 desiredRotation = CalculateDesiredRotation (
			horizontalViewStickInput,
			verticalViewStickInput,
			pivotPoint
		);

		float currentXAngle = transform.rotation.eulerAngles.x;
		bool angleTooLow = currentXAngle < 10.0f ||
			( 320.0f < currentXAngle &&
				currentXAngle < 360.0f );

		if ( verticalViewStickInput < 0.0f && angleTooLow ) {
			desiredRotation.x = currentXAngle;
		}

		Quaternion newRotation = Quaternion.Slerp (
			transform.rotation,
			Quaternion.Euler ( desiredRotation ),
			cameraMovementSpeed * Time.deltaTime
		);
			
		Vector3 newPosition = pivotPoint + ( newRotation * cameraDistanceFromTarget );

		transform.rotation = newRotation;
		transform.position = newPosition;
	}

	private Vector3 CalculateDesiredRotation (
		float horizontalViewStickInput,
		float verticalViewStickInput,
		Vector3 pivotPoint
	) {
		Vector3 desiredRotation;
		if ( cameraMode.Equals ( CameraMode.THIRD_PERSON ) ) {
			desiredRotation = new Vector3 (
				verticalViewStickInput * cameraMovementSpeed,
				horizontalViewStickInput * cameraMovementSpeed,
				0.0f
			);
		} 

		else if ( cameraMode.Equals ( CameraMode.SIDE_SCROLLING_XY ) ) {
			desiredRotation = Vector3.zero;
		}

		else if ( cameraMode.Equals ( CameraMode.SIDE_SCROLLING_ZY ) ) {
			desiredRotation = Vector3.up * -90f;
		}

		else {
			desiredRotation = transform.rotation.eulerAngles;
		}
			
		return desiredRotation;
	}

	public virtual void SetCameraMovementSpeed ( float cameraMovementSpeed ) {
		this.cameraMovementSpeed = cameraMovementSpeed;
	}

	public virtual Vector3 GetCameraDistanceFromTarget () {
		return cameraDistanceFromTarget;
	}

	public virtual Vector3 GetCameraAngleFromTarget () {
		return cameraAngleFromTarget;
	}

	private void SetCameraDistanceFromTarget ( Vector3 cameraDistanceFromTarget ) {
		this.cameraDistanceFromTarget = cameraDistanceFromTarget;
	}

	private void SetCameraAngleFromTarget ( Vector3 angleFromTarget ) {
		// do nothing
	}

	public virtual Vector3 GetCameraUpDirection () {
		Vector3 cameraForwardDirection = transform.up;
		cameraForwardDirection.x = 0.0f;
		cameraForwardDirection.z = 0.0f;
		// return only y axis information
		return cameraForwardDirection.normalized;
	}

	public Vector3 GetCameraForwardDirection () {
		Vector3 cameraForwardDirection = transform.forward;
		cameraForwardDirection.y = 0.0f;

		// return only x and z axis information
		return cameraForwardDirection; //.normalized;
	}

	public Vector3 GetCameraSideDirection () {
		Vector3 cameraSideDirection = transform.right;

		// return only x and z axis information
		cameraSideDirection.y = 0.0f;

		return cameraSideDirection.normalized;
	}

	public void SetColliderAndRigidbody () {
		if ( GetComponent < Camera > () == null ) {
			return;
		} 

		SphereCollider cameraCollider = GetComponent < SphereCollider > ();
		if ( cameraCollider == null ) {
			cameraCollider = gameObject.AddComponent ( typeof ( SphereCollider ) ) as SphereCollider;
		}
		cameraCollider.radius *= 1.5f;
		cameraCollider.isTrigger = true;

		Rigidbody cameraRigidBody = GetComponent < Rigidbody > ();
		if ( cameraRigidBody == null ) {
			cameraRigidBody = gameObject.AddComponent ( typeof ( Rigidbody ) ) as Rigidbody;
		}
		cameraRigidBody.isKinematic = true;
		cameraRigidBody.freezeRotation = true;
	}

	public virtual CameraMode GetCameraMode () {
		return cameraMode;
	}
}