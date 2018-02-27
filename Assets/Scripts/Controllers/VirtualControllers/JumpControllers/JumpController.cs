using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JumpController : MonoBehaviour , IJumpController {
	private bool didDoubleJump = false;

	public virtual bool Jump ( Rigidbody rigidBody ) {
		if ( rigidBody == null ) {
			return false;
		}

		bool grounded = onGround ( rigidBody ); 

		if ( !grounded && CanDoubleJump () && didDoubleJump ) {
			return false;
		}

		else if ( !grounded && !CanDoubleJump () ) {
			return false;
		}

		if ( !grounded && CanDoubleJump () && !didDoubleJump ) {
			didDoubleJump = true;
		}
			
		ApplyJump ( rigidBody );
		return true;
	}

	private void ApplyJump ( Rigidbody rigidBody ) {
		float currentVelocityX = rigidBody.velocity [ 0 ];
		float currentVelocityY = rigidBody.velocity [ 1 ];
		float currentVelocityZ = rigidBody.velocity [ 2 ];

		Vector3 jumpVelocity = new Vector3 (
			currentVelocityX,
			currentVelocityY + JumpSpeed (),
			currentVelocityZ
		);

		rigidBody.velocity = jumpVelocity;
	}

	private bool onGround ( Rigidbody rigidBody ) {
		if ( rigidBody == null ) {
			return false;
		}

		Vector3 down = transform.TransformDirection ( Vector3.down );

		bool zeroVerticalVelocity = ( rigidBody.velocity.y == 0f );

		bool rayCastHitFloor = Physics.Raycast (
			rigidBody.position,
			down,
			1.0f
		);

		if ( !zeroVerticalVelocity && !rayCastHitFloor ) {
			return false;
		}

		didDoubleJump = false;
		return true;
	}

	protected abstract float JumpSpeed ();

	protected abstract bool CanDoubleJump ();
}
