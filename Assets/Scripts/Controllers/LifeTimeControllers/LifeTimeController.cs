using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LifeTimeController : MonoBehaviour , ILifeTimeController {
	ITimeManipulatableController timeManipulatableController;
	Timer lifeTimer;
	ILifeTimeListener lifeTimeListener;
	bool lifeTimerStarted = false;

	// Use this for initialization
	void Start () {
		timeManipulatableController = GetComponent < ITimeManipulatableController > ();
		if ( timeManipulatableController == null ) {
			timeManipulatableController = new NullTimeManipulatableController ();
		}

		lifeTimer = new Timer ( GetLifeTimeSec () );
		StartLifeTime ();
	}
	
	// Update is called once per frame
	void Update () {
		lifeTimer.updateTimer ( Time.deltaTime * timeManipulatableController.TimeFactor () );
	
		if ( lifeTimeListener != null && LifeTimeEnded () ) {
			NotifyLifeTimeListener ();
		}
	}

	public virtual void StartLifeTime () {
		lifeTimer.startTimer ();
		lifeTimerStarted = true;
	}

	public virtual float TimeRemainingSec () {
		return lifeTimer.currentTimeRemaining ();
	}

	public virtual float TimeRemainingDecimal () {
		if ( lifeTimer.timerAmountMax () == 0f ) {
			return 0f;
		}

		return lifeTimer.currentTimeRemaining () / lifeTimer.timerAmountMax ();
	}

	public virtual bool LifeTimeEnded () {
		return lifeTimerStarted && lifeTimer.stopped ();
	}

	public virtual void SetLifeTimeListener ( ILifeTimeListener lifeTimeListener ) {
		this.lifeTimeListener = lifeTimeListener;
		Debug.Log ( "listener added" );
	}

	private void NotifyLifeTimeListener () {
		if ( lifeTimeListener != null ) {
			lifeTimeListener.OnLifeTimeEnd ();
		} 

		else {
			Destroy ( gameObject );
		}
	}

	protected abstract float GetLifeTimeSec ();
}
