using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeManipulatorController : MonoBehaviour , ITimeManipulatorController , ILifeTimeListener {
	private const float TIME_STOP_EFFECT = 0f;
	private const float TIME_RESTORE_EFFECT = 1.0f;

	private ILifeTimeController lifeTimeController;

	private Collider timeFieldCollider;
	private Renderer timeFieldRenderer;

	private float changeFactor = 0.01f;
	float direction = 1f;

	private HashSet < ITimeManipulatableController > timeManipulatableObjects = new HashSet < ITimeManipulatableController > ();

	void Start () {
		lifeTimeController = GetComponent < ILifeTimeController > ();
		if ( lifeTimeController == null ) {
			lifeTimeController = new NullLifeTimeController ();
		}
		lifeTimeController.SetLifeTimeListener ( this );

		ManipulateTime ();
	}

	void Update () {
		float currentScale = transform.localScale.x;

		if ( TimeManipulationSphereScale () < currentScale ) {
			direction *= -1f; 
		} 

		else if ( currentScale < TimeManipulationSphereScale () - 0.5f ) {
			direction = 1f;
		}

		transform.localScale = new Vector3 ( 
			currentScale + ( changeFactor * direction ),
			currentScale + ( changeFactor * direction ),
			currentScale + ( changeFactor * direction )
		);
	}

	public virtual void ManipulateTime () {
		transform.localScale = new Vector3 ( 
			TimeManipulationSphereScale (),
			TimeManipulationSphereScale (),
			TimeManipulationSphereScale ()
		);

		timeFieldRenderer = GetComponent < Renderer > ();
		if ( timeFieldRenderer != null ) {
			timeFieldRenderer.material = ( Material ) Resources.Load (
				TimeManipulationSphereMaterial (), 
				typeof ( Material ) 
			);
			timeFieldRenderer.enabled = true;
		}

		timeFieldCollider = GetComponent < Collider > ();
		if ( timeFieldCollider != null ) {
			timeFieldCollider.isTrigger = true;
		}
	}

	public virtual void RestoreTime () {
		foreach ( ITimeManipulatableController virtualController in getTimeManipulatableObjects () ) {
			RestoreTimeManipulatableObject ( virtualController );
		}
	}

	public virtual bool ManipulateTimeManipulatableObject ( ITimeManipulatableController virtualController ) {
		if ( IsVirtualControllerDestroyed ( virtualController ) ) {
			return false;
		}

		virtualController.UpdateTimeFactor ( TimeManipulationFactor () );
		return timeManipulatableObjects.Add ( virtualController );
	}

	public virtual bool RestoreTimeManipulatableObject ( ITimeManipulatableController virtualController ) {
		if ( IsVirtualControllerDestroyed ( virtualController ) ) {
			return false;
		}

		virtualController.UpdateTimeFactor ( TimeRestoreFactor () );
		return timeManipulatableObjects.Remove ( virtualController );
	}

	private ITimeManipulatableController [] getTimeManipulatableObjects () {
		ITimeManipulatableController [] objects = new ITimeManipulatableController [ timeManipulatableObjects.Count ];
		timeManipulatableObjects.CopyTo ( objects );
		return objects;
	}
		
	private bool IsVirtualControllerDestroyed ( ITimeManipulatableController virtualController ) {
		return virtualController == null;
	}

	private float TimeRestoreFactor () {
		return TIME_RESTORE_EFFECT;
	}

	protected abstract float TimeManipulationFactor ();
	protected abstract float TimeManipulationSphereScale ();
	protected abstract string TimeManipulationSphereMaterial ();

	void OnTriggerEnter ( Collider collider ) {
		ITimeManipulatableController virtualController = 
			collider.GetComponent < ITimeManipulatableController > ();
		
		if ( collider.isTrigger || virtualController == null ) { 
			return;
		}
			
		ManipulateTimeManipulatableObject ( virtualController );

		// stop motion
		Rigidbody rigidBody = GetComponent < Rigidbody > ();
		if ( rigidBody != null ) {
			rigidBody.isKinematic = true;
		}
	}

	void OnTriggerExit ( Collider collider ) {
		ITimeManipulatableController virtualController = 
			collider.GetComponent < ITimeManipulatableController > ();

		if ( collider.isTrigger || virtualController == null ) { 
			return;
		}
			
		RestoreTimeManipulatableObject ( virtualController );
	}

	public virtual void OnLifeTimeEnd () {
		RestoreTime ();
		Destroy ( gameObject );
	}

}
