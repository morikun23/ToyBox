using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class ArmFreezeState : IArmState {

		public void OnEnter(ArmComponent arg_arm) {
			arg_arm.m_owner.Hand.m_graspingItem.OnGraspedEnter(arg_arm.m_owner);
		}

		public void OnUpdate(ArmComponent arg_arm) {
			
		}

		public void OnExit(ArmComponent arg_arm) {
			
		}

		public IArmState GetNextState(ArmComponent arg_arm) {
			if (arg_arm.m_shorten) {
				return new ArmShortenState();
			}
			return null;
		}
	}
}