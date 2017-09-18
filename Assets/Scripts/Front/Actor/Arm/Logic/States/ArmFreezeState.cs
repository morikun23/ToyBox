using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class ArmFreezeState : IArmState {

		public void OnEnter(Arm arg_arm) {
			Debug.Log("s");
		}

		public void OnUpdate(Arm arg_arm) {

		}

		public void OnExit(Arm arg_arm) {

		}

		public IArmState GetNextState(Arm arg_arm) {
			return new ArmShortenState();
		}
	}
}