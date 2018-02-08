using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWatcherInput : MonoBehaviour , IInputController {
	private ICameraController cameraController;
	private IVirtualController virtualController;
	private SphereCollider attackRadiusTrigger;

	private Timer attackTimer;
	private float attackWaitSec = 0.5f;

	private const string PROJECTILE_PREFAB = "Prefabs/Prototype 1 Enemies/Projectile";

	private float visionRadius = 15.0f;

	private float turnSpeed = 5.0f;

	void Start () {
		virtualController = GetComponent < IVirtualController > ();
		if ( virtualController == null ) {
			virtualController = new NullVirtualController ();
		}
		/*
		virtualController.SetMovementSpeedProperties (
			0f,
			0f,
			turnSpeed,
			0f
		);
		virtualController.SetRigidbodyProperties (
			true, 
			true
		);
		*/

		cameraController = new NullCameraController ();

		attackTimer = new Timer ( attackWaitSec );
		attackRadiusTrigger = gameObject.AddComponent < SphereCollider > ();
		attackRadiusTrigger.isTrigger = true;
		attackRadiusTrigger.radius = visionRadius;
	}

	void Update () {
		attackTimer.updateTimer ( Time.deltaTime * virtualController.CurrentTimeEffect () );
	}

	public virtual void SetCameraController ( ICameraController cameraController ) {
		// do nothing
	}

	public virtual void SetVirtualController ( IVirtualController virtualController ) {
		if ( virtualController == null ) {
			virtualController = new NullVirtualController ();
		}
		this.virtualController = virtualController;
	}

	public virtual void EnableInput () {
		// do nothing
	}

	public virtual void DisableInput () {
		// do nothing
	}

	public virtual void SetPosition ( Vector3 newPosition ) {
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
		
	void OnTriggerEnter ( Collider collider ) {
		PlayerInputController playerInputController = 
			collider.GetComponent < PlayerInputController > ();

		if ( playerInputController == null ) {
			return;
		}

		Quaternion desiredRotation = Quaternion.LookRotation (
			collider.transform.position,
			Vector3.up 
		);

		virtualController.DesiredRotation ( desiredRotation );

	}

	void OnTriggerStay ( Collider collider ) {
		PlayerInputController playerInputController = 
			collider.GetComponent < PlayerInputController > ();

		if ( playerInputController == null ) {
			return;
		}

		Quaternion desiredRotation = Quaternion.LookRotation (
			collider.transform.position,
			Vector3.up 
		);

		virtualController.DesiredRotation ( 
			Quaternion.LookRotation ( 
				transform.position - collider.transform.position
			)
		);

		if ( attackTimer.stopped () && virtualController.CurrentTimeEffect () != 0f ) {				
			GameObject projectile = ( GameObject ) Instantiate ( Resources.Load ( PROJECTILE_PREFAB ) );

			if ( projectile != null ) {
				Vector3 projectilePosition = transform.position;

				float playerYPosition = collider.transform.position.y;
				projectilePosition.y = playerYPosition;

				projectile.transform.position = projectilePosition;
				projectile.transform.up = transform.forward;
			}
		
			attackTimer.startTimer ();
		}

	}
}