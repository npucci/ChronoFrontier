using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInputController : MonoBehaviour , IInputController {
	private IAttackController attackController;
	private float lifeTimeSec = 2f;
	private Timer lifeTimer;
	private float attackDamage = 20.0f;
	private float projectileSpeed = 5f;
	private float projectileMaxSpeedBoost = 20f;

	private float indesctructableTimeSec = 0.2f;
	private Timer indestructableTimer;

	private IVirtualController virtualController;
	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		attackController = GetComponent < IAttackController > ();
		if ( attackController == null ) {
			attackController = new NullAttackController ();
		}

		virtualController = GetComponent < IVirtualController > ();
		if ( virtualController == null ) {
			virtualController = new NullVirtualController ();
		}
		virtualController.SetMovementSpeedProperties (
			projectileSpeed,
			projectileMaxSpeedBoost,
			0f,
			0f
		);

		lifeTimer = new Timer ( lifeTimeSec );
		lifeTimer.startTimer ();

		indestructableTimer = new Timer ( indesctructableTimeSec );
		indestructableTimer.startTimer ();
	}

	void Update () {
		virtualController.RunButton ( true );
		virtualController.MovementStickInput (
			0f,
			-1f,
			transform.forward,
			transform.up,
			transform.right
		);

		if ( lifeTimer.stopped () && virtualController.CurrentTimeEffect () != 0f ) {
			Destroy ( gameObject );
		}

		lifeTimer.updateTimer ( Time.deltaTime * virtualController.CurrentTimeEffect () );
		indestructableTimer.updateTimer ( Time.deltaTime * virtualController.CurrentTimeEffect () );
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

	void OnCollisionEnter ( Collision collision ) {
		if ( !indestructableTimer.stopped () ) {
			return;
		}

		IHealthController healthController = collision.gameObject.GetComponent < IHealthController > ();
		if ( healthController != null ) {
			attackController.LightAttack ( healthController );
		} 

		if ( virtualController.CurrentTimeEffect () != 0f ) {
			Destroy ( gameObject );
		}
	}
}
