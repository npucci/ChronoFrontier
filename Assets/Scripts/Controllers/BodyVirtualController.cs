using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Niccolo Pucci
 * Purpose:
 * Generic Finite-State Machine for game characters, 
 * that can be used for the player and NPC/AI
 * Use: Attach to desired mesh (with rigidbody)
*/

public class BodyVirtualController : MonoBehaviour , IVirtualController {
	private IHealthController healthController;
	private ICombatController combatController;
	private IInteractionController interactionController;
	private ITimeManipulatorController timeManipulatorController;

	private Rigidbody rigidBody;
	private float bodyMass = 54.0f;
	private float drag = 0.5f;
	private float angularDrag = 0.5f;

	private float normalSpeed = 12.0f;

	private bool sliding = false;

	private bool running = false;
	private float runSpeedMax = 5.0f;
	private float runSpeedIncrement = 0.5f;
	private float runSpeedCurrent = 0.0f;

	private float turnSpeed = 15.2f;
	private float jumpSpeed = 10.0f;

	private float maxHP = 100.0f;
	private float currentHP = 100.0f;

	private bool interacting = false;

	private bool attacking = false;
	private bool magicAttacking = false;
	private float attackDamage = 15.0f;

	private bool timeSlowing = false;
	private bool timePausing = false;
	private bool timeStopping = false;
	private float slowDownEffect = 1.0f; 

	private BodyState bodyState = BodyState.Idle;
	private ActivityState activityState = ActivityState.Idle;

	// vertical state: grounded, falling, rising
	// movement state: running, walking, jumping, sliding, none
	// activity state: sleeping, interacting, attacking, moving, idle
	// combat state: sword attacking, magic attacking, time slowing, time pausing, time stopping
	// time state: time slowed, time paused, time stopped

	private enum BodyState {
		Idle,
		Walking,
		Running,
		Moving,
		Jumping,
		Sliding,
		Sleeping
	}

	private enum ActivityState {
		Idle,
		SwordAttacking,
		MagicAttacking,
		Interacting,
		TimeSlowing,
		TimePausing,
		TimeStopping
	}


	void Start () {
		rigidBody = GetComponent < Rigidbody > ();
		if ( rigidBody == null ) {
			rigidBody = gameObject.AddComponent < Rigidbody > ();
		}
		rigidBody.mass = bodyMass;
		rigidBody.drag = drag;
		rigidBody.angularDrag = angularDrag;
		rigidBody.freezeRotation = true;
		rigidBody.isKinematic = false;

		healthController = GetComponent < IHealthController > ();
		if ( healthController == null ) {
			healthController = new NullHealthController ();
		}
		healthController.SetCurrentHP ( currentHP );
		healthController.SetMaxHP ( maxHP );

		combatController = GetComponent < ICombatController > ();
		if ( combatController == null ) {
			combatController = new NullCombatController ();
		}
		combatController.SetAttackDamage (attackDamage);

		interactionController = GetComponent < IInteractionController > ();
		if ( interactionController == null ) {
			interactionController = new NullInteractionController ();
		}
			
		timeManipulatorController = GetComponent < ITimeManipulatorController > ();
		if ( timeManipulatorController == null ) {
			timeManipulatorController = new NullTimeManipulatorController ();
		}
	}

	void Update () {
		/*
		bool noVelocity = rigidBody.velocity.x == 0.0f && 
			rigidBody.velocity.y == 0.0f && 
			rigidBody.velocity.z == 0.0f;
		
		if ( noVelocity ) {
			bodyState = BodyState.Idle;
		}
		*/
	}

	public virtual void MovementStickInput (
		float horizontalMovementStickInput,
		float verticalMovementStickInput,
		Vector3 forwardDirection,
		Vector3 sideDirection
	) {
		if ( !running && 0.0f < runSpeedCurrent ) {
			runSpeedCurrent -= runSpeedIncrement;

			if ( runSpeedCurrent < 0.0f ) {
				runSpeedCurrent = 0.0f;
			}
		} 

		else if ( running && runSpeedCurrent < runSpeedMax )  {
			runSpeedCurrent += runSpeedIncrement;

			if ( runSpeedMax < runSpeedCurrent ) {
				runSpeedCurrent = runSpeedMax;
			}
		}

		Move (
			normalSpeed + runSpeedCurrent,
			horizontalMovementStickInput,
			verticalMovementStickInput,
			forwardDirection,
			sideDirection
		);
	}

	public virtual void RunButton (  bool clicked ) {
		if ( onGround () ) {
			running = clicked;
		} 
	}

	public virtual void Slide ( bool clicked ) {
		if ( onGround () ) {
			sliding = clicked;
		} 
	}

	public virtual void JumpButton ( bool clicked ) {
		if ( rigidBody == null || !onGround () || !clicked ) {
			return;
		}

		float currentVelocityX = rigidBody.velocity [ 0 ];
		float currentVelocityZ = rigidBody.velocity [ 2 ];

		Vector3 jumpVelocity = new Vector3 (
			currentVelocityX,
			jumpSpeed,
			currentVelocityZ
		);

		rigidBody.velocity = jumpVelocity;
	}

	public virtual void InteractionButton ( bool clicked ) {
		interacting = clicked;
	}

	public virtual void MagicButton ( bool clicked ) {
		magicAttacking = clicked;
	}

	private void Move (
		float movementSpeed,
		float horizontalMovementStickInput,
		float verticalMovementStickInput,
		Vector3 forwardDirection,
		Vector3 sideDirection
	) {
		
		if ( rigidBody == null ) {
			return;
		}

		if ( slowDownEffect == 0.0f ) {
			return;
		}
	
		if ( horizontalMovementStickInput == 0.0f && verticalMovementStickInput == 0.0f ) {
			return;
		}
			
		movementSpeed *= slowDownEffect;

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
			Vector3.up 
		);

		// 7. check if desired rotation is not the zero vector: no change in rotation if so
		if ( !desiredRotation.eulerAngles.Equals ( Vector3.zero ) ) {
			Quaternion newRotation = Quaternion.Slerp ( 
				rigidBody.rotation,
				desiredRotation,
				turnSpeed * Time.deltaTime
			);

			// 8. maintain original xz-axis rotation
			newRotation.x = rigidBody.rotation.x;
			newRotation.z = rigidBody.rotation.z;

			// 9. apply new rotation to rigidBody
			rigidBody.rotation = newRotation;
		}

		// 10. maintain original y-axis velocity, i.e. gravity or jumping 
		newVelocity.y = rigidBody.velocity.y * slowDownEffect;

		// 11. apply new velocity to rigidBody
		rigidBody.velocity = newVelocity;
	}
		
	private bool onGround () {
		Vector3 down = transform.TransformDirection ( Vector3.down );
	
		if ( Physics.Raycast (
			rigidBody.position,
			down,
			1.0f
		) ) {
			return true;
		}

		return rigidBody.velocity.y == 0.0f;
	}

	public Vector3 GetPosition () {
		return rigidBody.position;
	}

	public virtual void TimeStatusEffect ( float slowDownEffect ) {
		this.slowDownEffect = slowDownEffect;

		if ( slowDownEffect == 0.0f ) {
			rigidBody.isKinematic = true;
		} 

		else {
			rigidBody.isKinematic = false;
		}
	}

	public virtual void TimeSlowButton ( bool clicked ) {
		timeSlowing = clicked;
		if ( timeSlowing ) {
			timeManipulatorController.TimeSlow ();	
		}

		else if ( !timePausing && !timeStopping ) {
			timeManipulatorController.TimeRestore ();
		}
	}

	public virtual void TimePauseButton ( bool clicked ) {
		timePausing = clicked;
		if ( timePausing ) {
			timeManipulatorController.TimePause ();
		}

		else if ( !timeSlowing && !timeStopping ) {
			timeManipulatorController.TimeRestore ();
		}
	}

	public virtual void TimeStopButton ( bool clicked ) {
		timeStopping = clicked;
		if ( timeSlowing ) {
			timeManipulatorController.TimeStop ();	
		}

		else if ( !timePausing && !timeSlowing ) {
			timeManipulatorController.TimeRestore ();
		}
	}

	void OnTriggerEnter ( Collider collider ) {
		IVirtualController virtualController = 
			collider.GetComponent < BodyVirtualController > ();
		
		if ( virtualController == null ) { 
			return;
		}

		if ( !timeSlowing && !timePausing && !timeStopping ) {
			return;
		}
			
		timeManipulatorController.AddTimeManipulatableObject ( virtualController );
	}

	void OnTriggerStay ( Collider collider ) {
		IVirtualController virtualController = 
			collider.GetComponent < BodyVirtualController > ();

		if ( virtualController == null ) { 
			return;
		}

		if ( !timeSlowing && !timePausing && !timeStopping ) {
			return;
		}

		timeManipulatorController.AddTimeManipulatableObject ( virtualController );
	}

	void OnTriggerExit ( Collider collider ) {
		IVirtualController virtualController = 
			collider.GetComponent < IVirtualController > ();
		if ( virtualController != null ) {
			timeManipulatorController.RemoveTimeManipulatableObject ( virtualController );
		}
	}
}
