using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeProjectileVirtualController : BodyVirtualController {
	protected override float GetBodyMass () {
		return 0.0f;
	}

	protected override float GetDrag () {
		return 0f;
	} 

	protected override float GetAngularDrag () {
		return 0f;
	} 

	protected override bool IsFreezeRotation () {
		return true;
	} 

	protected override bool IsKinematic () {
		return false;
	} 

	protected override bool UseGravity () {
		return false;
	} 

	protected override float GetNormalSpeed () {
		return 1f;
	} 

	protected override float GetRunSpeedMax () {
		return 0f;
	} 

	protected override float GetRunSpeedIncrement () {
		return 0f;
	} 

	protected override float GetTurnSpeed () {
		return 0f;
	} 

	protected override float GetJumpSpeed () {
		return 0f;
	} 

	protected override float GetMaxHP () {
		return 0f;
	} 

	protected override float GetAttackWaitSec () {
		return 0f;
	} 

	protected override float GetAttackWindowSec () {
		return 0f;
	} 

	protected override float GetAttackDamage () {
		return 0f;
	} 
}
