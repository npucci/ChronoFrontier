using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DashController : MonoBehaviour , IDashController {
	bool dashing = false;
	private float dashSpeedMax;
	private float dashSpeedIncrement;
	private float currentDashSpeed = 0f;

	public virtual void Dash ( bool dashing ) {
		if ( !dashing && 0.0f < currentDashSpeed ) {
			currentDashSpeed -= DashSpeedIncrement ();

			if ( currentDashSpeed < 0.0f ) {
				currentDashSpeed = 0.0f;
			}
		}

		else if ( dashing && currentDashSpeed < MaxDashSpeed () )  {
			currentDashSpeed += DashSpeedIncrement ();

			if ( MaxDashSpeed () < currentDashSpeed ) {
				currentDashSpeed = MaxDashSpeed ();
			}
		}
	}

	public virtual float DashSpeed () {
		return currentDashSpeed;
	}

	protected abstract float MaxDashSpeed ();
	protected abstract float DashSpeedIncrement ();
}
