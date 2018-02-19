using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyVirtualController : BodyVirtualController {
	protected override float GetBodyMass () {
		return 54.0f;
	}

	protected override float GetDrag () {
		return 0.5f;
	} 

	protected override float GetAngularDrag () {
		return 0.5f;
	} 

	protected override float GetNormalSpeed () {
		return 12.0f;
	} 

	protected override float GetRunSpeedMax () {
		return 5.0f;
	} 

	protected override float GetRunSpeedIncrement () {
		return 0.5f;
	} 

	protected override float GetTurnSpeed () {
		return 15.2f;
	} 

	protected override float GetJumpSpeed () {
		return 10.0f;
	} 
		
	protected override float GetMaxHP () {
		return 100.0f;
	} 

	protected override float GetAttackWaitSec () {
		return 0.2f;
	} 

	protected override float GetAttackWindowSec () {
		return 0.1f;
	} 

	protected override float GetAttackDamage () {
		return 15.0f;
	} 
}
