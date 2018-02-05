using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulatorController : MonoBehaviour , ITimeManipulatorController {
	private const float TIME_SLOW_EFFECT = 0.15f;
	private const float TIME_PAUSE_EFFECT = 0f;
	private const float TIME_STOP_EFFECT = 0f;
	private const float TIME_RESTORE_EFFECT = 1.0f;

	private float currentTimeStatusEffect = 1.0f;

	private float timeFieldRadiusMultiplier = 10.0f;
	private SphereCollider timeField;

	private HashSet < IVirtualController > timeManipulatableObjects = new HashSet < IVirtualController > ();

	void Start () {
		timeField = gameObject.AddComponent < SphereCollider > ();
		timeField.isTrigger = true;
		timeField.radius = timeFieldRadiusMultiplier * timeField.radius;
	}

	public virtual void TimeSlow () {
		currentTimeStatusEffect = TIME_SLOW_EFFECT;
		foreach ( IVirtualController timeManipulatableObject in getTimeManipulatableObjects () ) {
			if ( timeManipulatableObject != null ) {
				timeManipulatableObject.TimeStatusEffect ( currentTimeStatusEffect );	
			} 

			else {
				RemoveTimeManipulatableObject ( timeManipulatableObject );
			}
		}
	}

	public virtual void TimePause () {
		currentTimeStatusEffect = TIME_PAUSE_EFFECT;
		foreach ( IVirtualController timeManipulatableObject in getTimeManipulatableObjects () ) {
			if ( timeManipulatableObject != null ) {
				timeManipulatableObject.TimeStatusEffect ( currentTimeStatusEffect );	
			} 

			else {
				RemoveTimeManipulatableObject ( timeManipulatableObject );
			}
		}
	}

	public virtual void TimeStop () {
		currentTimeStatusEffect = TIME_STOP_EFFECT;
		foreach ( IVirtualController timeManipulatableObject in getTimeManipulatableObjects () ) {
			if ( timeManipulatableObject != null ) {
				timeManipulatableObject.TimeStatusEffect ( currentTimeStatusEffect );
			} 

			else {
				RemoveTimeManipulatableObject ( timeManipulatableObject );
			}
		}
	}

	public virtual void TimeRestore () {
		currentTimeStatusEffect = TIME_RESTORE_EFFECT;

		foreach ( IVirtualController timeManipulatableObject in getTimeManipulatableObjects () ) {
			if ( timeManipulatableObject != null ) {
				timeManipulatableObject.TimeStatusEffect ( currentTimeStatusEffect );
			} 

			RemoveTimeManipulatableObject ( timeManipulatableObject );
		}
	}

	public virtual void AddTimeManipulatableObject ( IVirtualController virtualController ) {
		if ( virtualController != null && !timeManipulatableObjects.Contains ( virtualController ) ) {
			timeManipulatableObjects.Add ( virtualController );
			virtualController.TimeStatusEffect ( currentTimeStatusEffect );
		}
	}

	public virtual void RemoveTimeManipulatableObject ( IVirtualController virtualController ) {
		if ( virtualController != null && timeManipulatableObjects.Contains ( virtualController ) ) {
			virtualController.TimeStatusEffect ( TIME_RESTORE_EFFECT );
			timeManipulatableObjects.Remove ( virtualController );
		}
	}

	private IVirtualController [] getTimeManipulatableObjects () {
		IVirtualController [] objects = new IVirtualController [ timeManipulatableObjects.Count ];
		timeManipulatableObjects.CopyTo ( objects );
		return objects;
	}
}
