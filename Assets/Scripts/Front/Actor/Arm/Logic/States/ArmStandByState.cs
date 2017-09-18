using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class ArmStandByState : IArmState {

		public void OnEnter(Arm arg_arm) {

		}

		public void OnUpdate(Arm arg_arm) {

		}

		public void OnExit(Arm arg_arm) {

		}

		public IArmState GetNextState(Arm arg_arm) {
			if (arg_arm.lengthen) {
				return new ArmLengthenState(0.2f);
			}
			return null;
		}
	}
}