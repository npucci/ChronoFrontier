using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegularJumpController : JumpController {
	protected override float JumpSpeed () {
		return 11f;
	}

	protected override bool CanDoubleJump () {
		return false;
	}
}
