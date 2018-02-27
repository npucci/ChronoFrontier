using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpController : JumpController {
	protected override float JumpSpeed () {
		return 11f;
	}

	protected override bool CanDoubleJump () {
		return true;
	}
}