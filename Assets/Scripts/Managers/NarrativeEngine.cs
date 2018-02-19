using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeEngine : MonoBehaviour , ICameraModeTriggerListener {
	private IInputController playerInputController;
	private IHealthController playerHealthController;
	private IVirtualController playerVirtualController;
	private ICameraController cameraController;

	private GameObject specialBox;

	private Timer playerInputDisableTimer = null;
	private float playerInputDisableTimerWaitSec = 0.2f;

	void Start () {

		Camera camera = Camera.main;
		if ( camera == null ) {
			GameObject cameraGameObject = Instantiate ( new GameObject () );
			camera = cameraGameObject.AddComponent < Camera > ();
		}

		cameraController = camera.GetComponent < ICameraController > ();
		if ( cameraController == null ) {
			cameraController = camera.gameObject.AddComponent < DynamicCameraController > ();
		}

		specialBox = GameObject.FindGameObjectWithTag ( "Special Box" );
		if ( specialBox == null ) {
			specialBox = new GameObject ();
		}

		GameObject player = GameObject.FindGameObjectWithTag ( "Player" );
		playerInputController = player.GetComponent < IInputController > (); 
		if ( player == null || playerInputController == null ) {
			playerInputController = new NullInputController ();
		}
		playerInputController.SetCameraController ( cameraController );
		playerInputController.SetCameraMode ( 
			CameraMode.THIRD_PERSON,
			Vector3.zero
		);

		playerVirtualController = player.GetComponent < IVirtualController > (); 
		if ( player == null || playerVirtualController == null ) {
			playerVirtualController = new NullVirtualController ();
		}

		playerHealthController = player.GetComponent < IHealthController > ();
		if ( playerHealthController == null ) {
			playerHealthController = new NullHealthController ();
		}

		CameraModeTrigger [] cameraModeTriggers = ( CameraModeTrigger [] ) GameObject.FindObjectsOfType < CameraModeTrigger > ();
		foreach ( CameraModeTrigger cameraModeTrigger in cameraModeTriggers ) {
			cameraModeTrigger.SetCameraModeListener ( this );
		}
	}

	void Update () {
		//Debug.Log ( "playerHealthController.GetCurrentHP () = " + playerHealthController.GetCurrentHP () );
		if ( playerInputController.GetPosition ().y < -10.0f ) {
			SpawnPlayer ();
		}

		if ( specialBox.transform.position.y < -10.0f ) {
			SpawnBox ();
		}

		if ( playerInputDisableTimer != null ) {
			playerInputDisableTimer.updateTimer ( Time.deltaTime );
		}

		if ( playerInputDisableTimer != null && playerInputDisableTimer.stopped () ) {
			playerInputController.EnableInput ();
			playerInputDisableTimer = null;
		}
	}

	private void SpawnPlayer () {
		playerInputController.SetPosition ( Vector3.up * 10.0f );
		playerHealthController.SetToMaxHP ();
		playerInputController.SetCameraMode ( 
			CameraMode.THIRD_PERSON,
			Vector3.zero
		);
	}

	private void SpawnBox () {
		specialBox.transform.position = new Vector3 (
			0f,
			10f,
			21f
		);
	}

	public virtual void OnCameraModeTrigger ( 
		CameraMode cameraMode, 
		Vector3 newForwardDirection 
	) {
		Debug.Log ( "cameraMode = " + cameraMode );

		if ( !cameraMode.Equals ( CameraMode.NONE ) ) {
			playerInputController.DisableInput ();

			playerInputDisableTimer = new Timer ( playerInputDisableTimerWaitSec );
			playerInputDisableTimer.startTimer ();

			playerInputController.SetCameraMode ( 
				cameraMode,
				newForwardDirection
			);
		}
	}
}
