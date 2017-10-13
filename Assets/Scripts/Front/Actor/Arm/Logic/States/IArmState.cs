using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public interface IArmState {

		void OnEnter(ArmComponent arg_arm);

		void OnUpdate(ArmComponent arg_arm);

		void OnExit(ArmComponent arg_arm);

		IArmState GetNextState(ArmComponent arg_arm);
	}
}