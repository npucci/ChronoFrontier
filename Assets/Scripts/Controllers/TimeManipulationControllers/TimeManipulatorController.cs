using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulatorController : MonoBehaviour , ITimeManipulatorController {


	private const float TIME_SLOW_EFFECT = 0.15f;
	private const float TIME_PAUSE_EFFECT = 0f;
	private const float TIME_STOP_EFFECT = 0f;
	private const float TIME_RESTORE_EFFECT = 1.0f;

	private float currentTimeStatusEffect = 1.0f;

	private float maxTimeFieldSphereScale = 7.0f;

	private GameObject timeFieldSphere;
	private SphereCollider timeField;
	private Renderer timeFieldRenderer;
	private const string TIME_FIELD_MATERIAL_SLOW = "Materials/TimeFieldSlow";
	private const string TIME_FIELD_MATERIAL_PAUSE = "Materials/TimeFieldPause";

	private HashSet < BodyVirtualController > timeManipulatableObjects = new HashSet < BodyVirtualController > ();

	void Start () {
		timeFieldSphere = GameObject.CreatePrimitive ( PrimitiveType.Sphere );
		timeFieldSphere.transform.localScale = new Vector3 ( 
			maxTimeFieldSphereScale,
			maxTimeFieldSphereScale,
			maxTimeFieldSphereScale
		);
		timeFieldSphere.transform.position = transform.position;
		timeFieldSphere.transform.SetParent ( gameObject.transform );

		timeFieldRenderer = timeFieldSphere.gameObject.GetComponent < Renderer > ();
		timeFieldRenderer.enabled = false;
		timeFieldRenderer.material = ( Material ) Resources.Load (
			null, 
			typeof( Material ) 
		);

		timeField = timeFieldSphere.GetComponent < SphereCollider > ();
		timeField.isTrigger = true;
	}

	public virtual void TimeSlow () {
		currentTimeStatusEffect = TIME_SLOW_EFFECT;
		foreach ( BodyVirtualController virtualController in getTimeManipulatableObjects () ) {
			if ( !IsVirtualControllerDestroyed ( virtualController ) ) {
				virtualController.TimeStatusEffect ( currentTimeStatusEffect );	
			} 

			else {
				RemoveTimeManipulatableObject ( virtualController );
			}
		}

		timeFieldRenderer.material = ( Material ) Resources.Load (
			TIME_FIELD_MATERIAL_SLOW, 
			typeof ( Material ) 
		);
		timeFieldRenderer.enabled = true;
	}

	public virtual void TimePause () {
		currentTimeStatusEffect = TIME_PAUSE_EFFECT;
		foreach ( BodyVirtualController virtualController in getTimeManipulatableObjects () ) {
			if ( !IsVirtualControllerDestroyed ( virtualController ) ) {
				virtualController.TimeStatusEffect ( currentTimeStatusEffect );	
			} 

			else {
				RemoveTimeManipulatableObject ( virtualController );
			}
		}

		timeFieldRenderer.material = ( Material ) Resources.Load (
			TIME_FIELD_MATERIAL_PAUSE, 
			typeof ( Material ) 
		);
		timeFieldRenderer.enabled = true;
	}

	public virtual void TimeStop () {
		currentTimeStatusEffect = TIME_STOP_EFFECT;
		foreach ( BodyVirtualController virtualController in getTimeManipulatableObjects () ) {
			if ( !IsVirtualControllerDestroyed ( virtualController ) ) {
				virtualController.TimeStatusEffect ( currentTimeStatusEffect );
			} 

			else {
				RemoveTimeManipulatableObject ( virtualController );
			}
		}

		timeFieldRenderer.material = ( Material ) Resources.Load (
			TIME_FIELD_MATERIAL_PAUSE, 
			typeof ( Material ) 
		);
		timeFieldRenderer.enabled = true;
	}

	public virtual void TimeRestore () {
		currentTimeStatusEffect = TIME_RESTORE_EFFECT;

		foreach ( BodyVirtualController virtualController in getTimeManipulatableObjects () ) {
			if ( !IsVirtualControllerDestroyed ( virtualController ) ) {
				virtualController.TimeStatusEffect ( currentTimeStatusEffect );
			} 

			RemoveTimeManipulatableObject ( virtualController );
		}

		timeFieldRenderer.material = ( Material ) Resources.Load (
			null, 
			typeof ( Material ) 
		);
		timeFieldRenderer.enabled = false;
	}

	public virtual void AddTimeManipulatableObject ( BodyVirtualController virtualController ) {
		if ( !IsVirtualControllerDestroyed ( virtualController ) && !timeManipulatableObjects.Contains ( virtualController ) ) {
			timeManipulatableObjects.Add ( virtualController );
			virtualController.TimeStatusEffect ( currentTimeStatusEffect );
		}
	}

	public virtual void RemoveTimeManipulatableObject ( BodyVirtualController virtualController ) {
		if ( !IsVirtualControllerDestroyed ( virtualController ) && timeManipulatableObjects.Contains ( virtualController ) ) {
			virtualController.TimeStatusEffect ( TIME_RESTORE_EFFECT );
		}

		timeManipulatableObjects.Remove ( virtualController );
	}

	private BodyVirtualController [] getTimeManipulatableObjects () {
		BodyVirtualController [] objects = new BodyVirtualController [ timeManipulatableObjects.Count ];
		timeManipulatableObjects.CopyTo ( objects );
		return objects;
	}
		
	private bool IsVirtualControllerDestroyed ( BodyVirtualController virtualController ) {
		return virtualController == null || virtualController.gameObject == null;
	}
}
