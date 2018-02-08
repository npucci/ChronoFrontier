using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraModeTrigger : MonoBehaviour , ICameraModeTrigger {
	protected CameraMode fromCameraMode = CameraMode.NONE;
	protected CameraMode toCameraMode = CameraMode.NONE;
	protected float colliderTriggerHeightFactor = 10.0f;

	private ICameraModeTriggerListener cameraModeTriggerListener = new NullCameraModeTriggerListener ();

	protected void Start () {
		BoxCollider collider = gameObject.AddComponent < BoxCollider > ();
		collider.size += Vector3.up * colliderTriggerHeightFactor; 
		collider.isTrigger = true;
	}

	public virtual void SetCameraModeListener ( ICameraModeTriggerListener cameraModeTriggerListener ) {
		if ( cameraModeTriggerListener == null ) {
			cameraModeTriggerListener = new NullCameraModeTriggerListener ();
		}
		this.cameraModeTriggerListener = cameraModeTriggerListener;
	}

	Vector3 ICameraModeTrigger.GetForwardDirection () {
		return GetForwardDirection ();
	}

	abstract public Vector3 GetForwardDirection ();

	public virtual CameraMode GetFromCameraMode () {
		return fromCameraMode;
	}

	public virtual CameraMode GetToCameraMode () {
		return toCameraMode;
	}

	private bool validCollider ( Collider collider ) {
		bool validColliderType = collider.GetType () == typeof ( BoxCollider ) || 
			collider.GetType () == typeof ( CapsuleCollider );
		return validColliderType;
	}

	private CameraMode DetermineCorrectCameraMode ( Vector3 position ) {
		float distanceX = transform.position.x - position.x;
		float distanceZ = transform.position.z - position.z;

		CameraMode cameraMode = CameraMode.NONE;

		if ( distanceX < 0.0f && 0.0f < distanceZ ) {
			cameraMode = toCameraMode;
		} 

		else {
			cameraMode = fromCameraMode;
		}
	
		return cameraMode;
	}

	void OnTriggerEnter ( Collider collider ) {
		PlayerInputController playerInputController = collider.GetComponent < PlayerInputController > ();
		if ( playerInputController == null || !validCollider ( collider ) ) {
			return;
		}

		Rigidbody rigidBody = playerInputController.gameObject.GetComponent < Rigidbody > ();
		if ( rigidBody != null ) {
			rigidBody.velocity -= rigidBody.velocity * 1.2f;
			rigidBody.angularVelocity -= rigidBody.angularVelocity * 1.2f;
		}

		CameraMode cameraMode = DetermineCorrectCameraMode ( collider.transform.position );

		cameraModeTriggerListener.OnCameraModeTrigger ( 
			cameraMode,
			GetForwardDirection () 
		);
	} 
}
