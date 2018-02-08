using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModeRestrictor : MonoBehaviour {
	protected CameraMode cameraMode = CameraMode.NONE;
	protected float colliderTriggerHeightFactor = 10.0f;

	protected void Start () {
		BoxCollider collider = gameObject.AddComponent < BoxCollider > ();
		collider.size += Vector3.up * colliderTriggerHeightFactor; 
		collider.isTrigger = true;
	}

	public CameraMode GetCameraMode () {
		return cameraMode;
	}

	private bool validCollider ( Collider collider ) {
		bool validColliderType = collider.GetType () == typeof ( BoxCollider ) || 
			collider.GetType () == typeof ( CapsuleCollider );
		return validColliderType;
	}

	private Vector3 CorrectedPositioning ( Vector3 initialPosition ) {
		Vector3 correctedPosition = initialPosition;
		if ( cameraMode.Equals ( CameraMode.SIDE_SCROLLING_XY ) ) {
			correctedPosition.z = transform.position.z;
		}

		else if ( cameraMode.Equals ( CameraMode.SIDE_SCROLLING_ZY ) ) {
			correctedPosition.x = transform.position.x;
		}

		return correctedPosition;
	}

	void OnTriggerEnter ( Collider collider ) {
		PlayerInputController playerInputController = collider.GetComponent < PlayerInputController > ();
		if ( playerInputController == null || !validCollider ( collider ) ) {
			return;
		}

		collider.transform.position = CorrectedPositioning ( collider.transform.position );
	} 

	void OnTriggerStay ( Collider collider ) {
		PlayerInputController playerInputController = collider.GetComponent < PlayerInputController > ();
		if ( playerInputController == null || !validCollider ( collider ) ) {
			return;
		}
			
		collider.transform.position = CorrectedPositioning ( collider.transform.position );
	} 
		
}

