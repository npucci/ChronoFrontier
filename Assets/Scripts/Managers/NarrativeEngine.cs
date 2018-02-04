using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeEngine : MonoBehaviour {
	private IInputController playerInputController;
	private ICameraController cameraController;

	void Start () {
		Camera camera = Camera.main;
		if ( camera == null ) {
			GameObject cameraGameObject = Instantiate ( new GameObject () );
			camera = cameraGameObject.AddComponent < Camera > ();
		}

		cameraController = camera.GetComponent < ICameraController > ();
		if ( cameraController == null ) {
			cameraController = camera.gameObject.AddComponent < ThirdPersonCameraController > ();
		}

		GameObject player = GameObject.FindGameObjectWithTag ( "Player" );
		playerInputController = player.GetComponent < IInputController > (); 
		if ( player == null || playerInputController == null ) {
			playerInputController = new NullInputController ();
			Debug.Log ( "player is null!" );
		}
		playerInputController.SetCameraController ( cameraController );
	}
}
