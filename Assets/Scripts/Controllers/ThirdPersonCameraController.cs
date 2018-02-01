using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour , ICameraController {
	private float cameraMovementSpeed;
	private Vector3 cameraAngleFromTarget;
	private Vector3 cameraDistanceFromTarget;

	void Start () {
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

		cameraMovementSpeed = 15.0f;

		setColliderAndRigidbody ();
	}

	public virtual void updateCameraPositioning ( 
		float horizontalViewStickInput,
		float verticalViewStickInput,
		Vector3 pivotPoint
	) {
		Vector3 desiredRotation = new Vector3 (
			verticalViewStickInput * cameraMovementSpeed,
			horizontalViewStickInput * cameraMovementSpeed,
			0.0f
		);

		float currentXAngle = transform.rotation.eulerAngles.x;
		bool angleTooLow = 320.0f < currentXAngle && 
			currentXAngle < 360.0f && 
			verticalViewStickInput < 0.0f;

		if ( angleTooLow ) {
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

	public virtual void setCameraMovementSpeed ( float cameraMovementSpeed ) {
		this.cameraMovementSpeed = cameraMovementSpeed;
	}

	public virtual Vector3 getCameraForwardVector () {
		Vector3 forwardDirection = transform.forward;
		return forwardDirection;
	}

	public virtual Vector3 getCameraDistanceFromTarget () {
		return cameraDistanceFromTarget;
	}

	public virtual Vector3 getCameraAngleFromTarget () {
		return cameraAngleFromTarget;
	}


	private void setCameraDistanceFromTarget ( Vector3 cameraDistanceFromTarget ) {
		this.cameraDistanceFromTarget = cameraDistanceFromTarget;
	}

	private void setCameraAngleFromTarget ( Vector3 angleFromTarget ) {

	}

	public Vector3 getCameraForwardDirection () {
		Vector3 cameraForwardDirection = transform.forward;
		cameraForwardDirection.y = 0.0f;
		// return only x and z axis information
		return cameraForwardDirection;
	}

	public Vector3 getCameraSideDirection () {
		Vector3 cameraSideDirection = transform.right;
		cameraSideDirection.y = 0.0f;
		// return only x and z axis information
		return cameraSideDirection;
	}

	public void setColliderAndRigidbody () {
		if ( GetComponent < Camera > () == null ) {
			return;
		} 

		SphereCollider cameraCollider = GetComponent < SphereCollider > ();
		if ( cameraCollider == null ) {
			cameraCollider = gameObject.AddComponent ( typeof ( SphereCollider ) ) as SphereCollider;
		}
		cameraCollider.isTrigger = true;

		Rigidbody cameraRigidBody = GetComponent < Rigidbody > ();
		if ( cameraRigidBody == null ) {
			cameraRigidBody = gameObject.AddComponent ( typeof ( Rigidbody ) ) as Rigidbody;
		}
		cameraRigidBody.isKinematic = true;
	}

	void OnCollisionEnter ( Collision collision ) {
		Debug.Log ( "OnCollisionEnter!" );
	}

	void OnTriggerEnter ( Collider collider ) {
		Debug.Log ( "OnTriggerEnter!" );
	}

}