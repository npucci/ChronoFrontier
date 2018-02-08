using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {
	private float timerAmount;
	private float countDown;
	private bool timerStarted;
	private bool stop = true;

	// default constructor
	public Timer () : this ( 0f ) {
		// do nothing
	}
		
	public Timer ( float timerAmount ) {
		this.timerAmount = timerAmount;
	}
		
	public void updateTimer ( float deltaTime ) {
		if ( !stop && countDown > 0f ) {
			countDown -= deltaTime;
			if ( countDown < 0f ) {
				stop = true;
			}
		}
	}
		
	public void startTimer () {
		countDown = timerAmount;
		stop = false;
	}

	public void restartTimer () {
		startTimer ();
	}
		
	public bool stopped () {
		return stop;
	}
		
	public void setTimer ( float timerAmount ) {
		this.timerAmount = timerAmount;
	}

	public float currentTimeRemaining () {
		return countDown;
	}
}