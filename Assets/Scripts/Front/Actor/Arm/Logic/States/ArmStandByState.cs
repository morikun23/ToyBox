using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class ArmStandByState : IArmState {

		public void OnEnter(ArmComponent arg_arm) {

		}

		public void OnUpdate(ArmComponent arg_arm) {

		}

		public void OnExit(ArmComponent arg_arm) {

		}

		public IArmState GetNextState(ArmComponent arg_arm) {
			if (arg_arm.m_isActive) {
				return new ArmLengthenState(0.2f);
			}
			return null;
		}
	}
}