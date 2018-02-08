using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationController {

	void Walk ();

	void Run ();

	void Crouch ();

	void Jump ();

	void Slide ();
}
