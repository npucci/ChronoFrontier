using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Niccolo Pucci
 * Purpose:
 * Generic Finite-State Machine for game characters, 
 * that can be used for the player and NPC/AI
 * Use: Attach to desired mesh
*/

public abstract class BodyVirtualController : MonoBehaviour , IVirtualController {
	private IHealthController healthController;
	private IInteractionController interactionController;
	private IJumpController jumpController;
	private IDashController dashController;
	private ITimeManipulatableController timeManipulatableController;
	private ICombatController combatController;
	private IProjectileAttackController timePauseProjectileAttackController;
	private IProjectileAttackController timeSlowProjectileAttackController;

	private Rigidbody rigidBody;

	private float normalSpeed;
	private float turnSpeed;

	private float maxHP;
	private float currentHP;

	private bool interacting = false;

	private bool attacking = false;
	private bool magicAttacking = false;
	private float attackDamage;

	private bool timeSlowing = false;
	private bool timePausing = false;
	private bool timeStopping = false;

	void Start () {
		normalSpeed = GetNormalSpeed ();
		turnSpeed = GetTurnSpeed ();

		maxHP = GetMaxHP ();

		rigidBody = GetComponent < Rigidbody > ();
		if ( rigidBody == null ) {
			rigidBody = gameObject.AddComponent < Rigidbody > ();
		}
		SetRigidbodyProperties ();

		healthController = GetComponent < IHealthController > ();
		if ( healthController == null ) {
			healthController = new NullHealthController ();
		}

		jumpController = GetComponent < IJumpController > ();
		if ( jumpController == null ) {
			jumpController = new NullJumpAttributes ();
		}

		dashController = GetComponent < IDashController > ();
		if ( dashController == null ) {
			dashController = new NullDashController ();
		}

		interactionController = GetComponent < IInteractionController > ();
		if ( interactionController == null ) {
			interactionController = new NullInteractionController ();
		}
			
		timeManipulatableController = GetComponent < ITimeManipulatableController > ();
		if ( timeManipulatableController == null ) {
			timeManipulatableController = new NullTimeManipulatableController ();
		}

		combatController = GetComponent < ICombatController > ();
		if ( combatController == null ) {
			combatController = new NullCombatController ();
		}
			
		timePauseProjectileAttackController = GetComponent < TimePauseProjectileAttackController > ();
		if ( timePauseProjectileAttackController == null ) {
			timePauseProjectileAttackController = new NullProjectileAttackController ();
		}
			
		timeSlowProjectileAttackController = GetComponent < TimeSlowProjectileAttackController > ();
		if ( timeSlowProjectileAttackController == null ) {
			timeSlowProjectileAttackController = new NullProjectileAttackController ();
		}
	}

	void FixedUpdate () {
		if ( timeManipulatableController.TimeFactor () != 0.0f && timeManipulatableController.TimeFactor () != 1.0f ) {
			rigidBody.AddForce ( 
				Physics.gravity * rigidBody.mass * rigidBody.velocity.y * Time.deltaTime,
				ForceMode.Acceleration
			);
		} 
	}

	public virtual void SetMovementSpeedProperties ( 
		float movementSpeed, 
		float runningSpeed, 
		float turnSpeed,
		float jumpingSpeed
	) {
		this.normalSpeed = movementSpeed;
		this.turnSpeed = turnSpeed;
	}

	public virtual void SetRigidbodyProperties () {
		rigidBody.mass = GetBodyMass ();
		rigidBody.drag = GetDrag ();
		rigidBody.angularDrag = GetAngularDrag ();
		rigidBody.freezeRotation = IsFreezeRotation ();
		rigidBody.isKinematic = IsKinematic ();
		rigidBody.useGravity = UseGravity ();
	}

	public virtual void MovementStickInput (
		float horizontalMovementStickInput,
		float verticalMovementStickInput,
		Vector3 upDirection,
		Vector3 forwardDirection,
		Vector3 sideDirection
	) {

		Move (
			normalSpeed + dashController.DashSpeed (),
			horizontalMovementStickInput,
			verticalMovementStickInput,
			upDirection,
			forwardDirection,
			sideDirection
		);
	}

	private void Move (
		float movementSpeed,
		float horizontalMovementStickInput,
		float verticalMovementStickInput,
		Vector3 upDirection,
		Vector3 forwardDirection,
		Vector3 sideDirection
	) {
		if ( rigidBody == null ) {
			return;
		}

		if ( timeManipulatableController.TimeFactor () == 0.0f ) {
			return;
		}

		bool hasInput = horizontalMovementStickInput != 0f || verticalMovementStickInput != 0f;
		if ( !hasInput ) {
			Vector3 slowedVelocity = rigidBody.velocity;
			slowedVelocity.x *= 0.9f;
			slowedVelocity.z *= 0.9f;
			rigidBody.velocity = slowedVelocity;
			return;
		}
			
		movementSpeed *= timeManipulatableController.TimeFactor ();

		// 1. get forward direction of camera
		Vector3 cameraForwardDirection = forwardDirection;

		// 2. get side direction of camera
		Vector3 cameraSideDirection = sideDirection;

		// 3. calculate new forward velocity
		Vector3 newForwardVelocity = cameraForwardDirection * verticalMovementStickInput * movementSpeed;

		// 4. calculate new side velocity
		Vector3 newSideVelocity = cameraSideDirection * horizontalMovementStickInput * movementSpeed;

		// 5. calculate new velocity
		Vector3 newVelocity = newForwardVelocity + newSideVelocity;

		// 6. calculate Slerp for new rotation, between the original velocity and the new velocity
		Quaternion desiredRotation = Quaternion.LookRotation ( 
			newVelocity,
			upDirection	
		);

		// 7. update rotation
		DesiredRotation ( desiredRotation );

		// 10. maintain original y-axis velocity, i.e. gravity or jumping 
		newVelocity.y = rigidBody.velocity.y * timeManipulatableController.TimeFactor ();

		// 11. apply new velocity to rigidBody
		rigidBody.velocity = newVelocity;
	}
		
	public virtual void DesiredRotation ( Quaternion desiredRotation ) {
		Quaternion newRotation = Quaternion.Slerp ( 
			rigidBody.rotation,
			desiredRotation,
			turnSpeed * Time.deltaTime * timeManipulatableController.TimeFactor ()	
		);

		// 8. maintain original xz-axis rotation
		newRotation.x = rigidBody.rotation.x;
		newRotation.z = rigidBody.rotation.z;

		// 9. apply new rotation to rigidBody
		rigidBody.MoveRotation ( newRotation );
	}

	public virtual void AttackButton ( bool clicked ) {
		// TODO: implement better combat system
	}

	public virtual void RunButton (  bool clicked ) {
		if ( onGround () ) {
			dashController.Dash ( clicked );
		}
	}

	public virtual void BoostButton ( bool clicked ) {
		if ( clicked ) {
			//boostController.ApplyBoost ( rigidBody );
		}
	}

	public virtual void JumpButton ( bool clicked ) {
		if ( clicked ) {
			jumpController.Jump ( rigidBody );
		}
	}

	public virtual void InteractionButton ( bool clicked ) {
		interacting = clicked;
	}

	public virtual void MagicButton ( bool clicked ) {
		magicAttacking = clicked;
	}
		
	private bool onGround () {
		Vector3 down = transform.TransformDirection ( Vector3.down );

		bool zeroVerticalVelocity = ( rigidBody.velocity.y == 0.0f );

		if ( zeroVerticalVelocity ) {
			return zeroVerticalVelocity;
		}

		bool rayCastHit = Physics.Raycast (
			rigidBody.position,
	        down,
	        1.0f
	    );

		return rayCastHit;
	}

	public Vector3 GetPosition () {
		return rigidBody.position;
	}

	public virtual void TimeSlowButton ( bool clicked ) {
		timeSlowProjectileAttackController.FireProjectile ( null );
	}

	public virtual void TimePauseButton ( bool clicked ) {
		timePauseProjectileAttackController.FireProjectile ( null );
	}

	public virtual void TimeStopButton ( bool clicked ) {
		timeStopping = clicked; // TODO: implement game pause
	}

	void OnTriggerEnter ( Collider collider ) {
		BodyVirtualController virtualController = 
			collider.GetComponent < BodyVirtualController > ();
		
		if ( collider.isTrigger || virtualController == null ) { 
			return;
		}

		IHealthController enemyHealthController = collider.GetComponent < IHealthController > ();
		float distanceX = Mathf.Abs ( rigidBody.position.x - collider.transform.position.x );
		float distanceZ = Mathf.Abs ( rigidBody.position.z - collider.transform.position.z );
		float distanceY = Mathf.Abs ( rigidBody.position.y - collider.transform.position.y );

		float xzDistanceMax = 0.1f;
		float yDistanceMax = 0.1f;
		bool validDistance = distanceX < xzDistanceMax && 
			distanceZ < xzDistanceMax && 
			distanceY < yDistanceMax;    

		if ( enemyHealthController != null ) {
			combatController.LightMeleeAttack ( enemyHealthController );
		}

		if ( !timeSlowing && !timePausing && !timeStopping ) {
			return;
		}
	}

	void OnTriggerStay ( Collider collider ) {
		BodyVirtualController virtualController = 
			collider.GetComponent < BodyVirtualController > ();
		
		if ( collider.isTrigger || virtualController == null ) { 
			return;
		}

		IHealthController enemyHealthController = collider.GetComponent < IHealthController > ();
		float distanceX = Mathf.Abs ( rigidBody.position.x - collider.transform.position.x );
		float distanceZ = Mathf.Abs ( rigidBody.position.z - collider.transform.position.z );
		float distanceY = Mathf.Abs ( rigidBody.position.y - collider.transform.position.y );

		float xzDistanceMax = 0.1f;
		float yDistanceMax = 0.1f;
		bool validDistance = distanceX < xzDistanceMax && 
			distanceZ < xzDistanceMax && 
			distanceY < yDistanceMax;    

		if ( enemyHealthController != null ) {
			combatController.LightMeleeAttack ( enemyHealthController );
		}

		if ( timeSlowing || timePausing || timeStopping ) {
			//timeManipulatorController.AddTimeManipulatableObject ( virtualController );
		}
	}

	void OnTriggerExit ( Collider collider ) {
		BodyVirtualController virtualController = collider.GetComponent < BodyVirtualController > ();
		if ( virtualController != null ) {
			//timeManipulatorController.RemoveTimeManipulatableObject ( virtualController );
		}
	}

	protected abstract float GetBodyMass ();

	protected abstract float GetDrag ();

	protected abstract float GetAngularDrag ();

	protected abstract bool IsFreezeRotation ();

	protected abstract bool IsKinematic ();

	protected abstract bool UseGravity ();

	protected abstract float GetNormalSpeed ();

	protected abstract float GetRunSpeedMax ();

	protected abstract float GetRunSpeedIncrement ();

	protected abstract float GetTurnSpeed ();

	protected abstract float GetJumpSpeed ();

	protected abstract float GetMaxHP ();

	protected abstract float GetAttackWaitSec ();

	protected abstract float GetAttackWindowSec ();

	protected abstract float GetAttackDamage ();
}
